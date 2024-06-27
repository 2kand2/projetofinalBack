namespace WarehouseAPI.Domain.Entities
{
    public class AssetType
    {
        public AssetType() { }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<InstitutionalAsset> InstitutionalAssets { get; set; }
    }
}
