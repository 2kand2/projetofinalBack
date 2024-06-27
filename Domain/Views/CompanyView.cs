using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Domain.Views
{
    public class CompanyView
    {
        public CompanyView() { }

        public CompanyView(Company company)
        {
            Id = company.Id;
            Name = company.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

    }
}
