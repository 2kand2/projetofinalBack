using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces.Repositories;

namespace WarehouseAPI.Infra.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        #region Attributes
        private readonly AppDbContext _context;

        #endregion

        #region Constructor

        public LocationRepository(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods
        public void Create(Location location)
        {
            _context.Locations.AddAsync(location);
            SaveChanges();
        }

        public void Delete(Location location, int companyId)
        {
            var LocatonToDelete = _context.Locations.FirstOrDefault(l => l.Id == location.Id && l.CompanyId == companyId);

            if (LocatonToDelete == null)
            {
                throw new UnauthorizedAccessException("Ambiente não encontrado nesta empresa.");
            }

            // Aqui vamos verificar se o ambiente possuí algum item e envia um 

            //if (location.Items.Any())
            //{
            //    throw new InvalidOperationException("Cannot delete location with associated items.");
            //}
            _context.Locations.Remove(LocatonToDelete);
            SaveChanges();
        }

        public Location FindById(int id, int companyId)
        {
            return _context.Locations
                           .Include(l => l.Company)
                           .Include(l => l.LocationType)
                           .Where(l => l.Id == id && l.CompanyId == companyId)
                           .FirstOrDefault();
        }

        public List<Location> getAll(int companyId)
        {
            return _context.Locations
                             .Include(l => l.Company)
                             .Include(l => l.LocationType)
                           .Where(l => l.CompanyId == companyId).ToList();
        }


        public void Update(Location location, int companyId)
        {
            var locationToUpdate = _context.Locations.FirstOrDefault(l => l.Id == location.Id && l.CompanyId == companyId);
        
            if(locationToUpdate is null)
            {
                throw new Exception("Não foi encontrada nenhuma location");
            }

            locationToUpdate.Name = location.Name;
            locationToUpdate.Place = location.Place;
            locationToUpdate.LocationTypeId = location.LocationTypeId;
            locationToUpdate.CompanyId = companyId;

            _context.Locations.Update(locationToUpdate);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }


        #endregion
    }
}
