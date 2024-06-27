using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Domain.Models
{
    public class CompanyModel
    {
        public CompanyModel(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }


    }
}
