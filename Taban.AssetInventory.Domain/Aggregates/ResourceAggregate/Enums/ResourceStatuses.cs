using System.ComponentModel;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.Enums;

/// <summary>
/// وضعیت‌های منابع
/// </summary>
public enum ResourceStatuses
{
    /// <summary>
    /// آماده به کار
    /// </summary>
    [Description("آماده به کار")]
    Operative = 1,

    /// <summary>
    /// در حال کار
    /// </summary>
    [Description("در حال کار")]
    InOperation = 2,

    /// <summary>
    /// خراب
    /// </summary>
    [Description("خراب")]
    Breakdown = 3,

    /// <summary>
    /// در دست تعمیر
    /// </summary>
    [Description("در دست تعمیر")]
    InMaintenance = 4,

    /// <summary>
    /// غیرفعال
    /// </summary>
    [Description("غیرفعال")]
    Inactive = 5
}
