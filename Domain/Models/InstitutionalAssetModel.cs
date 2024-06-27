namespace WarehouseAPI.Domain.Models
{
    public class InstitutionalAssetModel
    {
        public InstitutionalAssetModel(string name, string serialNumber, int assetCode, string condition, int locationId, int assetTypeId)
        {
            Name = name;
            SerialNumber = serialNumber;
            AssetCode = assetCode;
            Condition = condition;
            LocationId = locationId;
            AssetTypeId = assetTypeId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int AssetCode { get; set; }
        public string Condition { get; set; }
        public int LocationId { get; set; }
        public int AssetTypeId { get; set; }
    }
}
