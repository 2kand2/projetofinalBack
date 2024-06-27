using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Infra.Models
{
    public class InstitutionalAssetConfiguration : IEntityTypeConfiguration<InstitutionalAsset>
    {
        public void Configure(EntityTypeBuilder<InstitutionalAsset> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name).IsRequired().HasMaxLength(255);
            builder.Property(i => i.SerialNumber).IsRequired().HasMaxLength(255); ;
            builder.Property(i => i.AssetCode).IsRequired();
            builder.Property(i => i.EntryDate).IsRequired().HasMaxLength(45);
            builder.Property(i => i.Condition).IsRequired().HasMaxLength(255);
            builder.Property(i => i.LocationId).IsRequired();
            builder.Property(i => i.AssetTypeId).IsRequired();

            builder.HasOne(x => x.Location)
                .WithMany(x => x.InstitutionalAssets)
                .HasForeignKey(x => x.LocationId);

            builder.HasOne(x => x.AssetType)
                .WithMany(x => x.InstitutionalAssets)
                .HasForeignKey(x => x.AssetTypeId);

        }
    }
}
