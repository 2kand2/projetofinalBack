using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;

namespace WarehouseAPI.Domain.Interfaces.Services
{
    public interface IInstitutionalAssetService
    {
        ActionResult<InstitutionalAssetView> GetInstitutionalAssetById(int id, int companyId);
        ActionResult<IEnumerable<InstitutionalAssetView>> GetAllInstitutionalAssets(int companyId);
        ActionResult<InstitutionalAssetWithLocationView> GetAllInstitutionalAssetsByLocation(int companyId, int locationId);
        ActionResult CreateInstitutionalAsset(InstitutionalAssetModel model);
        ActionResult UpdateInstitutionalAsset(int id, InstitutionalAssetModel model, int companyId);
        ActionResult DeleteInstitutionalAsset(int id, int companyId);
    }
}
