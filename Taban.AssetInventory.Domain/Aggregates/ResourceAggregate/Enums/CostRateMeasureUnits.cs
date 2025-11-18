using System.ComponentModel;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.Enums;

/// <summary>
/// واحدهای اندازه‌گیری نرخ هزینه
/// </summary>
public enum CostRateMeasureUnits
{
    /// <summary>
    /// ساعت
    /// </summary>
    [Description("ساعت")]
    Hour = 1,

    /// <summary>
    /// نفر/ساعت
    /// </summary>
    [Description("نفر/ساعت")]
    PersonHour= 2,
}
