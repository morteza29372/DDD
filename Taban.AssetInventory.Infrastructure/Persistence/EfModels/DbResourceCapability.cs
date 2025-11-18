namespace Taban.AssetInventory.Infrastructure.Persistence.EfModels;

/// <summary>
/// قابلیت‌های منبع
/// </summary>
public class DbResourceCapability
{
    public long Id { get; set; }

    public long ResourceId { get; set; }

    public DbResource Resource { get; set; }

    public long CapabilityId { get; set; }

    public DbCapability Capability { get; set; }
}
