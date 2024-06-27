using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces.Repositories;
using WarehouseAPI.Domain.Interfaces.Services;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;

namespace WarehouseAPI.Services
{
    public class AssetTypeService : IAssetTypeService
    {
        private readonly IAssetTypeRepository _assetTypeRepository;

        public AssetTypeService(IAssetTypeRepository assetTypeRepository)
        {
            _assetTypeRepository = assetTypeRepository;
        }

        public async Task<List<AssetTypeView>> GetAllAssetTypesAsync()
        {
            List<AssetType> assetTypes = await _assetTypeRepository.GetAllAsync();
            return assetTypes.ConvertAll(at => new AssetTypeView(at));
        }

        public async Task<AssetTypeView> GetAssetTypeByIdAsync(int id)
        {
            AssetType assetType = await _assetTypeRepository.FindByIdAsync(id);
            if (assetType == null)
                return null;

            return new AssetTypeView(assetType);
        }

        public async Task<bool> CreateAssetTypeAsync(AssetTypeModel model)
        {
            AssetType existingAssetType = await _assetTypeRepository.FindByNameAsync(model.Name);

            if(existingAssetType != null)
            {
                return false;
            }

            AssetType assetType = new AssetType
            {
                Name = model.Name,
            };

            await _assetTypeRepository.CreateAsync(assetType);
            await _assetTypeRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAssetTypeAsync(AssetTypeModel model)
        {
            var existingAssetType = await _assetTypeRepository.FindByIdAsync(model.Id);
            if (existingAssetType == null)
            {
                return false;
            }

            existingAssetType.Name = model.Name;

            _assetTypeRepository.Update(existingAssetType);
            await _assetTypeRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAssetTypeAsync(int id)
        {
            var assetType = await _assetTypeRepository.FindByIdAsync(id);
            if (assetType is null)
            {
                return false;
            }
                _assetTypeRepository.Delete(assetType);
                await _assetTypeRepository.SaveChangesAsync();

            return true;
        }
    }
}
