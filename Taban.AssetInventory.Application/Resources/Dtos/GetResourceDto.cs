

using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.Enums;

namespace Taban.AssetInventory.Application.Resources.Dtos;

public class GetResourceDto
{

    public long Id { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    public string Name { get; set; } = string.Empty;

    public long ConcurrencyStamp { get; set; }

    public int WorkCalendarId { get; set; }

    public string WorkCalendarName { get; set; } = string.Empty;

    /// <summary>
    ///درصد راندمان
    /// </summary>
    public decimal Efficiency { get; set; }

    /// <summary>
    /// ضریب عملکرد
    /// </summary>
    public decimal Performance { get; set; }

    /// <summary>
    /// ظرفیت
    /// </summary>
    public decimal Capacity { get; set; }

    public string CapacityMeasureUnitName { get; set; } = string.Empty;

    public long ResourceTypeId { get; set; }


    public string ResourceTypeName { get; set; } = string.Empty;

    /// <summary>
    /// نرخ هزینه
    /// </summary>
    public decimal CostRate { get; set; }

    /// <summary>
    /// شناسه
    /// واحد اندازه گیری نرخ هزینه
    /// </summary>
    public int CostRateMeasureUnitId { get; set; }

    /// <summary>
    /// واحد اندازه گیری نرخ هزینه
    /// </summary>
    public string CostRateMeasureUnitName { get; set; } = string.Empty;

    /// <summary>
    /// شناسه وضعیت منبع
    /// </summary>
    public int ResourceStatusId { get; set; }

    /// <summary>
    /// وضعیت منبع
    /// </summary>
    public string ResourceStatusName { get; set; } = string.Empty;


    public List<long> Capabilities { get; set; } = new();
}
