﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WarehouseAPI.Domain.Authentication.User;
using WarehouseAPI.Domain.Constants;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces;
using WarehouseAPI.Domain.Interfaces.Services;
using WarehouseAPI.Domain.Models.User;
using WarehouseAPI.Domain.Response;
using WarehouseAPI.Services.AppServices;

namespace WarehouseAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IHttpService httpService;


        public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ILogger<AuthController> logger, IHttpService httpService)
        {
            this._tokenService = tokenService;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._configuration = configuration;
            _logger = logger;
            this.httpService = httpService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email!);

            if (user is not null && await _userManager.CheckPasswordAsync(user, loginModel.Password!))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.NameIdentifier, user.Id!),
                    new Claim(ClaimTypes.UserData, user.CompanyId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenService.GenerateAcessToken(authClaims, _configuration);

                var refreshToken = _tokenService.GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

                var dateTimeNow = DateTime.UtcNow;
                var dateTimeNowWithAditionalMinutes = dateTimeNow.AddMinutes(refreshTokenValidityInMinutes);

                user.RefreshTokenExpriryTime = dateTimeNowWithAditionalMinutes;

                user.RefreshToken = refreshToken;

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                });
            }
            return Unauthorized();

        }

        //[Authorize(Policy = RoleNames.AdminOnly)]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email!);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User already exists!" });
            }

            ApplicationUser user = new()
            {
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Name = model.UserName,
                CompanyId = model.CompanyId,
            };

            var result = await _userManager.CreateAsync(user, model.Password!);
            
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User creation failed." });
            }

            return Ok(new Response { Status= "Sucess", Message= "Uer created successfully!"});
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {

            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? acessToken = tokenModel.AcessToken
                ?? throw new ArgumentNullException(nameof(tokenModel));

            string refreshToken = tokenModel.RefreshToken
                ?? throw new ArgumentNullException(nameof(tokenModel));

            ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredToken(acessToken!, _configuration);

            if (principal is null)
            {
                return BadRequest("Invalid acess token/refresh token");
            }

            ApplicationUser? user = await _userManager.FindByNameAsync(principal.Identity!.Name!);

            if (user == null || user.RefreshToken != refreshToken
                             || user.RefreshTokenExpriryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token/refresh token");
            }

            JwtSecurityToken newAccessToken = _tokenService.GenerateAcessToken(principal.Claims.ToList(), _configuration);

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                acessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken,
            });
        }

        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Revoke()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity!;
            Requester requester = httpService.GetRequester(identity);

            var user = await _userManager.FindByNameAsync(requester.Name);
            if (user == null) return BadRequest("Invalid username");

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [Authorize(Policy = RoleNames.AdminOnly)]
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (roleResult.Succeeded)
                {
                    _logger.LogInformation(1, "Roles Added");
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Sucess", Message = 
                        $"Role {roleName} added successfully"});
                }
                else
                {
                    _logger.LogInformation(2, "Error");
                    return StatusCode(StatusCodes.Status400BadRequest, 
                        new Response { Status = "Error", Message =
                        $"Issue adding the new {roleName} role"});
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { Status = "Error", Message =
                        $"Role already exist."});
            }
        }

        [Authorize(Policy = RoleNames.AdminOnly)]
        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);

                if (result.Succeeded)
                {
                    _logger.LogInformation(1, $"User {user.Email} added to the {roleName} role");
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message =
                        $"User {user.Email} added to the {roleName} role"});
                }
                else
                {
                    _logger.LogInformation(1, $"Error: Unable to add user {user.Email} to the {roleName} role");
                    return StatusCode(StatusCodes.Status400BadRequest, new Response
                    {
                        Status = "Error",
                        Message = $"Error: Unable to add user {user.Email} to the {roleName} role"
                    });
                }
            }

            return BadRequest(new { error = "Unable to find user" });

        }
    }
}
