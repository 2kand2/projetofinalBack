namespace WarehouseAPI.Domain.Views
{
    public class LocationWithAssetCountView
    {
        public LocationWithAssetCountView()
        {
        }

        public LocationWithAssetCountView(int id, string name, string place, string locationType, int locationTypeId, int assetCount)
        {
            Id = id;
            Name = name;
            Place = place;
            LocationType = locationType;
            LocationTypeId = locationTypeId;
            AssetCount = assetCount;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }

        public string  LocationType { get; set; }

        public int LocationTypeId { get; set; }

        public int AssetCount { get; set; }
    }
}
