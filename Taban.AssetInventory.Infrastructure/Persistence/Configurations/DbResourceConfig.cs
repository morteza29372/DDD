using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taban.AssetInventory.Infrastructure.Persistence.EfModels;

namespace Taban.AssetInventory.Infrastructure.Persistence.Configurations;

public class DbResourceConfig:IEntityTypeConfiguration<DbResource>
{
    public void Configure(EntityTypeBuilder<DbResource> builder)
    {
        builder.ToTable("Resource", b => b.IsTemporal());

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.ConcurrencyStamp)
            .IsRequired()
            .IsConcurrencyToken();

        // Indexes
        builder.HasIndex(r => r.IsDeleted)
            .HasFilter("[IsDeleted] = 0");

        builder.HasIndex(e => e.Name)
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

        // Relations
        builder.HasOne(d => d.CreatedByUser)
            .WithMany()
            .HasForeignKey(d => d.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.UpdatedByUser)
            .WithMany()
            .HasForeignKey(d => d.UpdatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.DeletedByUser)
            .WithMany()
            .HasForeignKey(d => d.DeletedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.ResourceType)
            .WithMany(d => d.Resources)
            .HasForeignKey(d => d.ResourceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(d => d.WorkCalendar)
            .WithMany(d => d.Resources)
            .HasForeignKey(d => d.WorkCalendarId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.CostRateMeasureUnit)
            .WithMany(d => d.Resources)
            .HasForeignKey(d => d.CostRateMeasureUnitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.ResourceStatus)
            .WithMany(d => d.Resources)
            .HasForeignKey(d => d.ResourceStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
