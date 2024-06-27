using Microsoft.AspNetCore.Hosting.Server;

namespace WarehouseAPI.Domain.Entities
{
    public class LocationType
    {
        public LocationType()
        {
        }

        public LocationType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id {  get; set; }
        
        public string Name { get; set; }

        public ICollection<Location> Locations { get; set; }

    }
}
