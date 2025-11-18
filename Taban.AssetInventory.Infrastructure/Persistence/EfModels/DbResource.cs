using System;
using System.Collections.Generic;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.Enums;
using Taban.AssetInventory.DomainClasses;
using Taban.AssetInventory.Infrastructure.Persistence.Common;

namespace Taban.AssetInventory.Infrastructure.Persistence.EfModels;

/// <summary>
/// منابع
/// </summary>
public class DbResource : ISoftDeletable, IAuditable, IConcurrencyStamp
{
    public long Id { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    public string Name { get; set; }


    public long ConcurrencyStamp { get; set; }

    /// <summary>
    /// شناسه
    /// <inheritdoc cref="DomainClasses.WorkCalendar"/>
    /// </summary>
    public int WorkCalendarId { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="DomainClasses.WorkCalendar"/>
    /// </summary>
    public WorkCalendar WorkCalendar { get; set; }

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

    /// <summary>
    /// شناسه
    /// <inheritdoc cref="DbResourceType"/>
    /// </summary>
    public long ResourceTypeId { get; set; }

    /// <summary>
    /// <inheritdoc cref="DbResourceType"/>
    /// </summary>
    public DbResourceType ResourceType { get; set; }

    /// <summary>
    /// نرخ هزینه
    /// </summary>
    public decimal CostRate { get; set; }

    /// <summary>
    /// شناسه
    /// واحد اندازه گیری نرخ هزینه
    /// </summary>
    public CostRateMeasureUnits CostRateMeasureUnitId  { get; set; }

    /// <summary>
    /// واحد اندازه گیری نرخ هزینه
    /// </summary>
    public DbCostRateMeasureUnit CostRateMeasureUnit { get; set; }

    /// <summary>
    /// شناسه وضعیت منبع
    /// </summary>
    public ResourceStatuses ResourceStatusId { get; set; }

    /// <summary>
    /// وضعیت منبع
    /// </summary>
    public DbResourceStatus ResourceStatus { get; set; }

    /// <summary>
    /// قابلیت‌ها
    /// </summary>
    public ICollection<DbResourceCapability> Capabilities { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAtUtc { get; set; }
    public int? DeletedByUserId { get; set; }
    public User DeletedByUser { get; set; }
    public DateTimeOffset CreatedAtUtc { get; set; }
    public int CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; }
    public DateTimeOffset? UpdatedAtUtc { get; set; }
    public int? UpdatedByUserId { get; set; }
    public User UpdatedByUser { get; set; }
}
