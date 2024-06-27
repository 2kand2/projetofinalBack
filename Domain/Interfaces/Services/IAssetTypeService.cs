using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;

namespace WarehouseAPI.Domain.Interfaces.Services
{
    public interface IAssetTypeService
    {
        Task<List<AssetTypeView>> GetAllAssetTypesAsync();
        Task<AssetTypeView> GetAssetTypeByIdAsync(int id);
        Task<bool> CreateAssetTypeAsync(AssetTypeModel model);
        Task<bool> UpdateAssetTypeAsync(AssetTypeModel model);
        Task<bool> DeleteAssetTypeAsync(int id);
    }
}
