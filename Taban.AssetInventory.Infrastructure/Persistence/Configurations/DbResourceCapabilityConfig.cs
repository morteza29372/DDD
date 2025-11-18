using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taban.AssetInventory.Infrastructure.Persistence.EfModels;

namespace Taban.AssetInventory.Infrastructure.Persistence.Configurations;

public class DbResourceCapabilityConfig : IEntityTypeConfiguration<DbResourceCapability>
{
    public void Configure(EntityTypeBuilder<DbResourceCapability> builder)
    {
        builder.ToTable("ResourceCapability");

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.HasOne(d => d.Resource)
            .WithMany(d => d.Capabilities)
            .HasForeignKey(d => d.ResourceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Capability)
            .WithMany(d => d.ResourceCapabilities)
            .HasForeignKey(d => d.CapabilityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(r => new { r.ResourceId, r.CapabilityId });
    }
}
