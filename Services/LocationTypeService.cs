using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces.Repositories;
using WarehouseAPI.Domain.Interfaces.Services;
using WarehouseAPI.Domain.Views;
using WarehouseAPI.Infra.Repositories;

namespace WarehouseAPI.Services
{
    public class LocationTypeService : ILocationTypeService
    {
        private readonly IRepository<LocationType>  _locationTypeRepository;

        public LocationTypeService(IRepository<LocationType> locationTypeRepository)
        {
            _locationTypeRepository = locationTypeRepository;
        }

        public ActionResult<List<LocationTypeView>> getAll()
        {
            var result = _locationTypeRepository.GetAll().ToList();

            if (result == null) 
            {
                return null;
            }

            List<LocationTypeView> locationTypeViews = new List<LocationTypeView>();

            foreach (var locationTypeView in result)
            {
                locationTypeViews.Add(new LocationTypeView(locationTypeView.Id, locationTypeView.Name));
            }


            return locationTypeViews;
        }
    }
}
