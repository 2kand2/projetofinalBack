using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces.Repositories;
using WarehouseAPI.Infra;

namespace WarehouseAPI.Infra.Repositories
{
    public class AssetTypeRepository : IAssetTypeRepository
    {
        private readonly AppDbContext _context;

        public AssetTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AssetType>> GetAllAsync()
        {
            return await _context.AssetTypes.ToListAsync();
        }

        public async Task<AssetType> FindByIdAsync(int id)
        {
            return await _context.AssetTypes.FindAsync(id);
        }

        public async  Task<AssetType> FindByNameAsync(string name)
        {
            return await _context.AssetTypes.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task CreateAsync(AssetType assetType)
        {
            await _context.AssetTypes.AddAsync(assetType);
        }

        public void Delete(AssetType assetType)
        {
            _context.AssetTypes.Remove(assetType);
        }

        public void Update(AssetType assetType)
        {
            _context.AssetTypes.Update(assetType);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
