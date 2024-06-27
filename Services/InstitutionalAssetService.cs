using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces.Repositories;
using WarehouseAPI.Domain.Interfaces.Services;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;

namespace WarehouseAPI.Services
{
    public class InstitutionalAssetService : IInstitutionalAssetService
    {
        private readonly IInstitutionalAssetRepository _repository;

        public InstitutionalAssetService(IInstitutionalAssetRepository repository)
        {
            _repository = repository;
        }


        public ActionResult<InstitutionalAssetView> GetInstitutionalAssetById(int id, int companyId)
        {
            InstitutionalAsset asset = _repository.FindById(id, companyId);
            if (asset == null)
            {
                return new NotFoundObjectResult($"InstitutionalAsset with id {id} not found.");
            }
            return new OkObjectResult(ToView(asset));
        }

        public ActionResult<IEnumerable<InstitutionalAssetView>> GetAllInstitutionalAssets(int companyId)
        {
            var assets = _repository.GetAll(companyId);
            var assetViews = assets.Select(a => ToView(a));
            return new OkObjectResult(assetViews);
        }
        

        public ActionResult<InstitutionalAssetWithLocationView> GetAllInstitutionalAssetsByLocation(int companyId, int locationId)
        {
            List<InstitutionalAsset> assets = _repository.GetAllByLocation(companyId, locationId).ToList();

            if(assets.Count == 0 || assets is null){
                return null;
            }

            List<InstitutionalAssetView> institutionalAssetViews = new List<InstitutionalAssetView>();

            foreach( var assetView in assets)
            {
                institutionalAssetViews.Add(ToView(assetView));
            }

            InstitutionalAssetWithLocationView institutionalAssetWithLocationView = new InstitutionalAssetWithLocationView(assets[0].Location.Name, institutionalAssetViews);

            return institutionalAssetWithLocationView;
            


        }

        public ActionResult CreateInstitutionalAsset(InstitutionalAssetModel model)
        {
            var existingAsset = _repository.GetAll(model.LocationId)
                .FirstOrDefault(a => a.Name == model.Name && a.SerialNumber == model.SerialNumber);
            if (existingAsset != null)
            {
                return new ConflictObjectResult($"InstitutionalAsset with the same name and serial number already exists.");
            }
            var dateTimeNow = DateTime.UtcNow;

            var asset = ToEntity(model, dateTimeNow);
            _repository.Add(asset);
            _repository.SaveChanges();
            return new OkObjectResult($"InstitutionalAsset {model.Name} created successfully.");
        }

        public ActionResult UpdateInstitutionalAsset(int id, InstitutionalAssetModel model, int companyId)
        {
            var existingAsset = _repository.FindById(id, companyId);
            if (existingAsset == null)
            {
                return new NotFoundObjectResult($"InstitutionalAsset with id {id} not found.");
            }

            existingAsset.Name = model.Name;
            existingAsset.SerialNumber = model.SerialNumber;
            existingAsset.AssetCode = model.AssetCode;
            existingAsset.EntryDate = DateTime.UtcNow;
            existingAsset.Condition = model.Condition;
            existingAsset.LocationId = model.LocationId;
            existingAsset.AssetTypeId = model.AssetTypeId;

            _repository.Update(existingAsset);
            _repository.SaveChanges();
            return new OkObjectResult($"InstitutionalAsset {model.Name} updated successfully.");
        }

        public ActionResult DeleteInstitutionalAsset(int id, int companyId)
        {
            var asset = _repository.FindById(id, companyId);
            if (asset == null)
            {
                return new NotFoundObjectResult($"InstitutionalAsset with id {id} not found.");
            }

            _repository.Remove(id, companyId);
            _repository.SaveChanges();
            return new OkObjectResult($"InstitutionalAsset with id {id} deleted successfully.");
        }

        private InstitutionalAsset ToEntity(InstitutionalAssetModel model, DateTime dateTime)
        {
            return new InstitutionalAsset
            {
                Id = model.Id,
                Name = model.Name,
                SerialNumber = model.SerialNumber,
                AssetCode = model.AssetCode,
                EntryDate = dateTime,
                Condition = model.Condition,
                LocationId = model.LocationId,
                AssetTypeId = model.AssetTypeId
            };
        }

        private InstitutionalAssetView ToView(InstitutionalAsset asset)
        {
            return new InstitutionalAssetView(
                asset.Id,
                asset.Name,
                asset.SerialNumber,
                asset.AssetCode,
                asset.EntryDate,
                asset.Condition,
                asset.Location.Name,
                asset.AssetType.Name
            );
        }

    }
}

