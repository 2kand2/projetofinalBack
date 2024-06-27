using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WarehouseAPI.Domain.Authentication.User;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces.Services;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;

namespace WarehouseAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {

        private ILocationService _locationService;
        private readonly IHttpService httpService;

        public LocationController(ILocationService locationService, IHttpService httpService)
        {
            _locationService = locationService;
            this.httpService = httpService;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateLocation")]
        public ActionResult Create([FromBody] LocationModel locationModel)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity!;
                Requester requester = httpService.GetRequester(identity);

                var result = _locationService.CreateLocation(locationModel, int.Parse(requester.CompaniesId));

                if (result.Value == false)
                {
                    return NotFound($"O tipo de ativo com o Id: {locationModel.Id} não foi encontrado");
                }

                return Ok($"O tipo de ativo {locationModel.Name} foi criado com sucesso!");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor {ex.Message}");
            }
        }
        
        
        [Authorize]
        [HttpGet]
        [Route("GetLocation/{id:int}")]
        public ActionResult GetLocation(int id)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity!;
                Requester requester = httpService.GetRequester(identity);

                var result = _locationService.GetLocation(id, int.Parse(requester.CompaniesId));

                if (result.Result is NoContentResult)
                {
                    return NoContent();
                }
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor {ex.Message}");
            }
        }
        
        
        [Authorize]
        [HttpGet]
        [Route("GetAllLocation")]
        public ActionResult GetAllLocation()
        {

            try
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity!;
                Requester requester = httpService.GetRequester(identity);

                int companyId = int.Parse(requester.CompaniesId);
                var result = _locationService.GetAllLocationByCompany(companyId);

                if (result.Result is NotFoundObjectResult || result.Result is BadRequestObjectResult)
                {
                    return result.Result;
                }

                var locationViews = result.Value;

                return Ok(locationViews);
            }
            catch
            {
                return StatusCode(500, $"Erro interno do servidor!");
            }
        }
        
        
        [Authorize]
        [HttpGet]
        [Route("GetLocationsWithQuantity")]
        public ActionResult GetLocationsWithCounts()
        {

            try
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity!;
                Requester requester = httpService.GetRequester(identity);

                int companyId = int.Parse(requester.CompaniesId);
                var result = _locationService.GetLocationsWithAssetCounts(companyId);

                if (result.Result is NotFoundObjectResult || result.Result is BadRequestObjectResult)
                {
                    return result.Result;
                }

                var locationViews = result.Value;

                return Ok(locationViews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor {ex.Message}");
            }
        }
        
        
        [Authorize]
        [HttpPut]
        public ActionResult UpdateLocation([FromBody] LocationModel locationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity!;
                Requester requester = httpService.GetRequester(identity);

                var result = _locationService.UpdateLocation(locationModel, int.Parse(requester.CompaniesId));

                if (result.Value == false)
                {
                    return NotFound($"O tipo de ativo com o Id: {locationModel.Id} não foi encontrado");
                }

                return Ok($"O tipo de ativo {locationModel.Name} foi criado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor {ex.Message}");
            }
        }
        
        
        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult DeleteLocation(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity!;
                Requester requester = httpService.GetRequester(identity);

                var result = _locationService.DeleteLocation(id, int.Parse(requester.CompaniesId));

                if (result.Value == false)
                {
                    return NotFound($"O tipo de ativo com o Id: {id} não foi encontrado");
                }


                return Ok($"O ambiente com o Id {id} foi removido com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor {ex.Message}");
            }
        }


    }
}
