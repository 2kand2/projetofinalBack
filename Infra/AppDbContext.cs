using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Infra.Models;

namespace WarehouseAPI.Infra
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Aqui vai os DBSets
        public DbSet<ApplicationUser> User {  get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<InstitutionalAsset> InstitutionalAssets { get; set; }

        public DbSet<AssetType> AssetTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new LocationConfiguration());
            builder.ApplyConfiguration(new InstitutionalAssetConfiguration());
        }

    }
}
