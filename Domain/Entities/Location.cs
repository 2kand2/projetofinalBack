namespace WarehouseAPI.Domain.Entities
{
    public class Location
    {
        public Location(){}

        public Location(int id, string name, string place, int locationTypeID, int companyId, Company company, LocationType locationType, ICollection<InstitutionalAsset> institutionalAssets)
        {
            Id = id;
            Name = name;
            Place = place;
            LocationTypeId = locationTypeID;
            CompanyId = companyId;
            Company = company;
            LocationType = locationType;
            InstitutionalAssets = institutionalAssets;
        }

        public int Id { get; set; } 

        public string Name { get; set; }
        public string Place { get; set; }
        public int LocationTypeId { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public LocationType LocationType { get; set; }

        public ICollection<InstitutionalAsset> InstitutionalAssets { get; set; }
    }
}