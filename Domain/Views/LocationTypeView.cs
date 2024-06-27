namespace WarehouseAPI.Domain.Views
{
    public class LocationTypeView
    {
        public LocationTypeView(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
