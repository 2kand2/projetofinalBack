using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WarehouseAPI.Domain.Authentication.User;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces.Services;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;
using WarehouseAPI.Services.AppServices;

namespace WarehouseAPI.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstitutionalAssetController : ControllerBase
    {
        private readonly IInstitutionalAssetService _service;
        private readonly IHttpService _httpService;

        public InstitutionalAssetController(IInstitutionalAssetService service, IHttpService httpService)
        {
            _service = service;
            _httpService = httpService;
        }

        private int GetCompanyIdFromClaims()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity!;
            Requester requester = _httpService.GetRequester(identity);
            return int.Parse(requester.CompaniesId);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<InstitutionalAssetView> GetById(int id)
        {
            int companyId = GetCompanyIdFromClaims();
            return _service.GetInstitutionalAssetById(id, companyId);
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<InstitutionalAssetView>> GetAll()
        {
            int companyId = GetCompanyIdFromClaims();
            return _service.GetAllInstitutionalAssets(companyId);
        }
        
        [Authorize]
        [HttpGet]
        [Route("GetAllInstitutionalAssetAndLocation/{locationId}")]
        public ActionResult<InstitutionalAssetWithLocationView> GetAll(int locationId)
        {
            int companyId = GetCompanyIdFromClaims();
            return _service.GetAllInstitutionalAssetsByLocation(companyId, locationId);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create([FromBody] InstitutionalAssetModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return _service.CreateInstitutionalAsset(model);
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] InstitutionalAssetModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int companyId = GetCompanyIdFromClaims();
            return _service.UpdateInstitutionalAsset(id, model, companyId);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            int companyId = GetCompanyIdFromClaims();
            return _service.DeleteInstitutionalAsset(id, companyId);
        }
    }
}
