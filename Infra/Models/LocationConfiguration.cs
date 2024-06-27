using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Infra.Models
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(x => x.Id);



            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Place).IsRequired();
            builder.Property(x => x.LocationTypeId).IsRequired();

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Locations)
                .HasForeignKey(x => x.CompanyId);


            builder.HasOne(x => x.LocationType)
                .WithMany(l => l.Locations)
                .HasForeignKey(x => x.LocationTypeId);
        }
    }
}
