using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Views;

namespace WarehouseAPI.Domain.Interfaces.Repositories
{
    public interface IInstitutionalAssetRepository
    {
        InstitutionalAsset FindById(int id, int companyId);
        
        IEnumerable<InstitutionalAsset> GetAll(int companyId);

        IEnumerable<InstitutionalAsset> GetAllByLocation(int companyId, int locationId);


        void Add(InstitutionalAsset asset);

        void Update(InstitutionalAsset asset);
        
        void Remove(int id, int companyId);
        
        void SaveChanges();
    }
}
