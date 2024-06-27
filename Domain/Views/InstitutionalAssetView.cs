namespace WarehouseAPI.Domain.Views
{
    public class InstitutionalAssetView
    {
        public InstitutionalAssetView(int id, string name, string serialNumber, int assetCode, DateTime entryDate, string condition, string location, string assetType)
        {
            Id = id;
            Name = name;
            SerialNumber = serialNumber;
            AssetCode = assetCode;
            EntryDate = entryDate;
            Condition = condition;
            Location = location;
            AssetType = assetType;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int AssetCode { get; set; }
        public DateTime EntryDate { get; set; }
        public string Condition { get; set; }
        public string Location { get; set; }
        public string AssetType { get; set; }
    }
}
