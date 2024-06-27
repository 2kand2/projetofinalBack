using WarehouseAPI.Domain.Models;

namespace WarehouseAPI.Domain.Entities
{
    public class Company
    {
        public Company(){}

        public Company(CompanyModel companyModel)
        {
            Id = companyModel.Id;
            Name = companyModel.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<Location> Locations { get; set; }

    }
}
