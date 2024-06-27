using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces.Repositories;
using WarehouseAPI.Domain.Interfaces.Services;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;
using WarehouseAPI.Infra.Repositories;

namespace WarehouseAPI.Services
{
    public class LocationService : ILocationService
    {

        private readonly ILocationRepository _locationRepository;
        private readonly IRepository<Company> _companyRepository;
        private readonly IInstitutionalAssetRepository _institutionalAssetRepository;

        public LocationService(ILocationRepository locationRepository, IRepository<Company> companyRepository, IInstitutionalAssetRepository institutionalAssetRepository = null)
        {
            _locationRepository = locationRepository;
            _companyRepository = companyRepository;
            _institutionalAssetRepository = institutionalAssetRepository;
        }

        public ActionResult<bool> CreateLocation(LocationModel locationModel, int idCompany)
        {
            
            Company company = _companyRepository.FindById(idCompany);

            if(company is null)
            {
                return false;
            }

            Location location = new Location
            {
                Name = locationModel.Name,
                Place = locationModel.Place,
                LocationTypeId = locationModel.TypeOfEnviroment,
                CompanyId = company.Id,
            };

            _locationRepository.Create(location);

            return true;
        }

        public ActionResult<bool> DeleteLocation(int id, int companyId)
        {
            Location locationToDelete = _locationRepository.FindById(id, companyId);

            if(locationToDelete is null)
            {
                return false;
            }

            _locationRepository.Delete(locationToDelete, companyId);

            return true;
        }

        public ActionResult<List<LocationView>> GetAllLocationByCompany(int idCompany)
        {

            Company company = _companyRepository.FindById(idCompany);

            if (company is null)
            {
                return new NotFoundObjectResult("Company not found");
            }

            List<Location> LocationList = _locationRepository.getAll(idCompany);

            if(LocationList is null || LocationList.Count == 0)
            {
                return new NotFoundObjectResult("No locations found for the specified company");
            }

            List<LocationView> locationViews = new List<LocationView>();



            foreach (Location location in LocationList)
            {
                locationViews.Add(new LocationView(location.Id, location.Name, location.Place, location.LocationType.Name, location.LocationTypeId));
            }

            return locationViews;
        }

        public ActionResult<LocationView> GetLocation(int id, int idCompany)
        {
            var location = _locationRepository.FindById(id, idCompany);

            if(location is null)
            {
                return new NoContentResult();
            }

            return new LocationView(location.Id, location.Name, location.Place, location.LocationType.Name, location.LocationTypeId);
        }   

        public ActionResult<bool> UpdateLocation(LocationModel locationModel, int companyId)
        {
            Location location = _locationRepository.FindById(locationModel.Id, companyId);

            if ( location is null)
            {
                return false;
            }

            Location locationToUpdate = new Location()
            {
                Id = locationModel.Id,
                Name = locationModel.Name,
                Place = locationModel.Place,
                LocationTypeId = locationModel.TypeOfEnviroment,
                CompanyId = companyId,
            };
            
            _locationRepository.Update(locationToUpdate, companyId);

            return true;
        }
        public ActionResult<List<LocationWithAssetCountView>> GetLocationsWithAssetCounts(int companyId)
        {

            var locations = _locationRepository.getAll(companyId);

            if (locations is null || locations.Count == 0)
            {
                return new List<LocationWithAssetCountView>();
            }

            var assets = _institutionalAssetRepository.GetAll(companyId);
            
            if (assets is null)
            {
                return new List<LocationWithAssetCountView>();
            }

            var assetsByLocation = assets
            .GroupBy(a => a.LocationId)
            .ToDictionary(g => g.Key, g => g.Count());

            var LocationWithAssetCountViews = locations.Select(l => new LocationWithAssetCountView(l.Id, l.Name, l.Place, l.LocationType.Name,l.LocationTypeId, assetsByLocation.ContainsKey(l.Id) ? assetsByLocation[l.Id] : 0)).ToList();

            return LocationWithAssetCountViews;
        }

        }

    }
