using Microsoft.AspNetCore.Identity;

namespace WarehouseAPI.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail {  get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpriryTime { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
