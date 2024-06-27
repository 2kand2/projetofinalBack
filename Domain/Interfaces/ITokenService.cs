using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WarehouseAPI.Domain.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Gera o Token JWT.
        /// </summary>
        /// <returns>Token JWT</returns>
        JwtSecurityToken GenerateAcessToken(IEnumerable<Claim> claims, IConfiguration _config);

        /// <summary>
        /// Atualiza o token jwt.
        /// </summary>
        /// <returns>Novo Token JWT</returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Extraí as claims do token jwt expirado.
        /// </summary>
        /// <returns>As claims do token Expirado.</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
    }
}
