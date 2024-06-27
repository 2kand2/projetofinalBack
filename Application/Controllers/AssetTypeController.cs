using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces.Services;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;

namespace WarehouseAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetTypeController : Controller
    {
        private readonly IAssetTypeService _assetTypeService;

        public AssetTypeController(IAssetTypeService assetTypeService)
        {
            _assetTypeService = assetTypeService;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateAssetType")]
        public async Task<ActionResult> Create([FromBody] AssetTypeModel assetType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _assetTypeService.CreateAssetTypeAsync(assetType);

                if (result == false)
                {
                    return NotFound($"O tipo de ativo com o Id: {assetType.Id} não foi encontrado");
                }

                return Ok($"O tipo de ativo {assetType.Name} foi criado com sucesso!");
            }
            catch
            {
                return StatusCode(500, $"Erro interno do servidor!");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAssetType/{id:int}")]
        public async Task<ActionResult> GetAssetType(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                AssetTypeView assetTypeView = await _assetTypeService.GetAssetTypeByIdAsync(id);

                if (assetTypeView == null)
                {
                    return NotFound($"Tipo de ativo com id {id} não encontrado.");
                }

                return Ok(assetTypeView);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor!");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAssetTypes")]
        public async Task<ActionResult> GetAllAssetTypes()
        {
            try
            {
                var assetTypesView = await _assetTypeService.GetAllAssetTypesAsync();

                if (assetTypesView == null || assetTypesView.Count == 0)
                {
                    return NoContent();
                }

                return Ok(assetTypesView);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor!");
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateAssetType")]
        public async Task<ActionResult> UpdateAssetType([FromBody] AssetTypeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _assetTypeService.UpdateAssetTypeAsync(model);

                if (result == false)
                {
                    return NotFound($"O tipo de ativo com o Id: {model.Id} não foi encontrado");
                }
                return Ok($"O tipo de ativo {model.Name} foi atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor!");
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteAssetType(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _assetTypeService.DeleteAssetTypeAsync(id);

                if (result == false)
                {
                    return NotFound($"O tipo de ativo com o Id: {id} não foi encontrado");
                }

                return Ok($"O tipo de ativo com id {id} foi removido com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor!");
            }
        }
    }
}
