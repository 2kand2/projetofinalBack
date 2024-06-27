using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces.Repositories;

namespace WarehouseAPI.Infra.Repositories
{
    public class InstitutionalAssetRepository : IInstitutionalAssetRepository
    {
        private readonly AppDbContext _context;

        public InstitutionalAssetRepository(AppDbContext context)
        {
            _context = context;
        }

        public InstitutionalAsset FindById(int id, int companyId)
        {
            return _context.InstitutionalAssets
                .Include(i => i.AssetType)
                .Include(i => i.Location)
                .ThenInclude(a => a.Company)
                .FirstOrDefault(i => i.Id == id && i.Location.CompanyId == companyId);
        }

        public IEnumerable<InstitutionalAsset> GetAll(int companyId)
        {
            return _context.InstitutionalAssets
            .Include(a => a.Location)
            .Include(a => a.AssetType)
            .Where(a => a.Location.CompanyId == companyId)
            .ToList();
        }

        public IEnumerable<InstitutionalAsset> GetAllByLocation(int companyId, int locationId)
        {
            return _context.InstitutionalAssets
                .Include(a => a.Location)
                .Include(a => a.AssetType)
                .Where(a => a.Location.Id == locationId && a.Location.Company.Id == companyId);
        }

        public void Add(InstitutionalAsset asset)
        {
            _context.InstitutionalAssets.Add(asset);
        }

        public void Update(InstitutionalAsset asset)
        {
            _context.InstitutionalAssets.Update(asset);
        }

        public void Remove(int id, int companyId)
        {
            var asset = FindById(id, companyId);
            if (asset != null)
            {
                _context.InstitutionalAssets.Remove(asset);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
