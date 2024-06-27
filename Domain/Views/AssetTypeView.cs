using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Domain.Views
{
    public class AssetTypeView
    {
        public AssetTypeView(AssetType assetType)
        {
            Id = assetType.Id;
            Name = assetType.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
