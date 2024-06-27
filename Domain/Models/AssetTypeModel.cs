using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Domain.Models
{
    public class AssetTypeModel
    {
        public AssetTypeModel()
        {
            // Construtor sem parâmetros
        }

        public AssetTypeModel(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }

}

