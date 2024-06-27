using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;

namespace WarehouseAPI.Domain.Interfaces.Services
{
    public interface ILocationService
    {
        ActionResult<List<LocationView>> GetAllLocationByCompany(int idCompany);

        ActionResult<List<LocationWithAssetCountView>> GetLocationsWithAssetCounts(int companyId);

        ActionResult<LocationView> GetLocation(int id, int idCompany);

        ActionResult<bool> CreateLocation(LocationModel locationModel, int idCompany);

        ActionResult<bool> DeleteLocation(int id, int idCompany);

        ActionResult<bool> UpdateLocation(LocationModel locationModel, int companyId);

    }
}