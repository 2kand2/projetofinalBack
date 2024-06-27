using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Domain.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarehouseAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationTypeController : ControllerBase
    {
        private readonly ILocationTypeService _locationTypeService;

        public LocationTypeController(ILocationTypeService locationTypeService)
        {
            _locationTypeService = locationTypeService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
           return Ok(_locationTypeService.getAll().Value);
        }
    }
}
