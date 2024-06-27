namespace WarehouseAPI.Domain.Entities
{
    public class InstitutionalAsset
    {
        public InstitutionalAsset() { }
        public InstitutionalAsset(int id, string name, string serialNumber, int assetCode, DateTime entryDate, string condition, int locationId, int assetTypeId)
        {
            Id = id;
            Name = name;
            SerialNumber = serialNumber;
            AssetCode = assetCode;
            EntryDate = entryDate;
            Condition = condition;
            LocationId = locationId;
            AssetTypeId = assetTypeId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int AssetCode { get; set; }
        public DateTime EntryDate { get; set; }
        public string Condition { get; set; }
        public int LocationId { get; set; }
        public int AssetTypeId { get; set; }


        public Location Location { get; set; }
        public AssetType AssetType { get; set; }
    }
}
