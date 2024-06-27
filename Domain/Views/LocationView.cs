using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Domain.Views
{
    public class LocationView
    {
        public LocationView() { }

        public LocationView(int id, string name, string place, string locationType, int locationTypeId)
        {
            Id = id;
            Name = name;
            Place = place;
            LocationType = locationType;
            LocationTypeId = locationTypeId;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }
        public string LocationType { get; set; }

        public int LocationTypeId { get; set; }
    }
}
