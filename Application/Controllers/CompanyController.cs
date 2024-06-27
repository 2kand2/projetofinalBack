using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Domain.Constants;
using WarehouseAPI.Domain.Interfaces.Services;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;

namespace WarehouseAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // [Authorize(Policy = RoleNames.AdminOnly)]
        [HttpPost]
        [Route("CreateCompany")]

        public ActionResult Create([FromBody] CompanyModel companyModel)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                CompanyView companyView = _companyService.Create(companyModel);

                if(companyView is null)
                {
                    return BadRequest("Não foi possível criar a Empresa!");
                }

                return Ok(companyView);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }            
        }


        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult CompanyFindById(int id)
        {
            try
            {
                CompanyView companyView = _companyService.FindById(id);

                if(companyView == null)
                {
                    return NotFound($"Não foi possível encontrar a empresa com o id {id}");
                }

                return Ok(companyView);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");

            }
        }


        [Authorize(Policy = RoleNames.AdminOnly)]
        [HttpPut]
        public ActionResult Update([FromBody] CompanyModel companyModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Não foi possível atualizar a Empresa!");
            }

            try
            {
                CompanyView companyView = _companyService.Update(companyModel);

                if (companyView == null)
                {
                    return NotFound($"Não foi possível encontrar a empresa {companyModel.Name}");
                }

                return Ok($"A empresa foi atualizada com sucesso!");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        [Authorize(Policy = RoleNames.AdminOnly)]
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                CompanyView companyView = _companyService.Delete(id);

                if (companyView is null) 
                {
                    return NotFound("Company not found!");
                }

                return Ok($"Company {companyView.Name} is deleted successfully.");


            }catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

    }
}
