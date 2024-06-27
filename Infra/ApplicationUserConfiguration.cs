using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Infra
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Email).IsRequired();

            builder.HasOne(x => x.Company)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.CompanyId);
        }
    }
}
