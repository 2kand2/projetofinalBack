namespace WarehouseAPI.Domain.Authentication.User
{
    public class Requester
    {
        public Requester() { }

        public Requester(string id, string name, string role, string companiesId)
        {
            Id = id;
            Name = name;
            CompaniesId = companiesId;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public string CompaniesId { get; set; }

    }
}
