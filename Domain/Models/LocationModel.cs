namespace WarehouseAPI.Domain.Models
{
    public class LocationModel
    {
        public LocationModel() { }

        public LocationModel(int id, string name, string place, int typeOfEnviroment)
        {
            Id = id;
            Name = name;
            Place = place;
            TypeOfEnviroment = typeOfEnviroment;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }
        public int TypeOfEnviroment { get; set; }
    }
}
