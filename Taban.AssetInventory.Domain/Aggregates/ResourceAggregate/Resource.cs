using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.Enums;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;
using Taban.AssetInventory.Domain.Aggregates.ResourceTypeAggregate.ValueObjects;
using Taban.AssetInventory.Domain.Aggregates.WorkCalendarAggregate.ValueObjects;
using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate;

public class Resource : AggregateRoot<ResourceId>
{
    public Resource(
        ResourceId id,
        ResourceName name,
        WorkCalendarId workCalendarId,
        ResourceEfficiency efficiency,
        ResourcePerformance performance,
        ResourceCapacity capacity,
        ResourceTypeId resourceTypeId,
        ResourceCostRate costRate,
        CostRateMeasureUnits costRateMeasureUnitId,
        ResourceStatuses resourceStatusId,
        List<ResourceCapability>? capabilities
            ) : base(id)
    {
        Id = id;
        Name = name;
        WorkCalendarId = workCalendarId;
        Efficiency = efficiency;
        Performance = performance;
        Capacity = capacity;
        ResourceTypeId = resourceTypeId;
        CostRate = costRate;
        CostRateMeasureUnitId = costRateMeasureUnitId;
        ResourceStatusId = resourceStatusId;
        _resourceCapabilities = capabilities ?? new List<ResourceCapability>();
    }

    /// <summary>
    /// نام
    /// </summary>
    public ResourceName Name { get; private set; }

    public void UpdateName(ResourceName name)
    {
        if (Name.Equals(name))
            return;

        Name = name;
    }

    /// <summary>
    /// تقویم
    /// </summary>
    public WorkCalendarId WorkCalendarId { get; private set; }

    public void UpdateWorkCalendarId(WorkCalendarId workCalendarId)
    {
        if (WorkCalendarId.Equals(workCalendarId))
            return;

        WorkCalendarId = workCalendarId;
    }

    /// <summary>
    ///درصد راندمان
    /// </summary>
    public ResourceEfficiency Efficiency { get; private set; }

    public void UpdateEfficiency(ResourceEfficiency efficiency)
    {
        if (Efficiency.Equals(efficiency))
            return;

        Efficiency = efficiency;
    }

    /// <summary>
    /// ضریب عملکرد
    /// </summary>
    public ResourcePerformance Performance { get; private set; }

    public void UpdatePerformance(ResourcePerformance performance)
    {
        if (Performance.Equals(performance))
            return;

        Performance = performance;
    }

    /// <summary>
    /// ظرفیت
    /// </summary>
    public ResourceCapacity Capacity { get; private set; }

    public void UpdateCapacity(ResourceCapacity capacity)
    {
        if (Capacity.Equals(capacity))
            return;

        Capacity = capacity;
    }

    /// <summary>
    /// شناسه نوع منبع
    /// </summary>
    public ResourceTypeId ResourceTypeId { get; private set; }

    public void UpdateResourceTypeId(ResourceTypeId resourceTypeId)
    {
        if (ResourceTypeId.Equals(resourceTypeId))
            return;

        ResourceTypeId = resourceTypeId;
    }

    /// <summary>
    /// نرخ هزینه
    /// </summary>
    public ResourceCostRate CostRate { get; private set; }

    public void UpdateCostRate(ResourceCostRate costRate)
    {
        if (CostRate.Equals(costRate))
            return;

        CostRate = costRate;
    }

    /// <summary>
    /// شناسه
    /// واحد اندازه گیری نرخ هزینه
    /// </summary>
    public CostRateMeasureUnits CostRateMeasureUnitId { get; private set; }

    public void UpdateCostRateMeasureUnitId(CostRateMeasureUnits costRateMeasureUnitId)
    {
        if (CostRateMeasureUnitId.Equals(costRateMeasureUnitId))
            return;

        CostRateMeasureUnitId = costRateMeasureUnitId;
    }

    /// <summary>
    /// شناسه وضعیت منبع
    /// </summary>
    public ResourceStatuses ResourceStatusId { get; private set; }

    public void UpdateResourceStatusId(ResourceStatuses resourceStatusId)
    {
        if (ResourceStatusId.Equals(resourceStatusId))
            return;

        ResourceStatusId = resourceStatusId;
    }


    // فیلد خصوصی قابلیت ها 
    private readonly List<ResourceCapability> _resourceCapabilities = [];

    /// <summary>
    /// قابلیت ها
    /// </summary>
    public IReadOnlyCollection<ResourceCapability> ResourceCapabilities => _resourceCapabilities;

    // متد برای اضافه کردن قابلیت ها
    public void AddResourceCapability(ResourceCapability newResourceCapability)
    {
        if (newResourceCapability is null)
            throw new ArgumentNullException(nameof(newResourceCapability));

        _resourceCapabilities.Add(newResourceCapability);
    }

    public ResourceCapability? GetCapabilityById(ResourceCapabilityId id)
    {
        var existingLinet = _resourceCapabilities
            .FirstOrDefault(d => d.Id == id);

        return existingLinet;
    }

    public void RemoveResourceCapability(ResourceCapabilityId lineId)
    {
        // پیدا کردن ردیف شناسه
        var lineToRemove = _resourceCapabilities.FirstOrDefault(d => d.Id == lineId);

        if (lineToRemove is null)
        {
            // اگر ردیف با شناسه مورد نظر یافت نشد، استثنا پرتاب می‌شود.
            throw new DomainException($"قابلیت با شناسه {lineId} یافت نشد.");
        }

        // حذف ردیف از لیست داخلی
        _resourceCapabilities.Remove(lineToRemove);
    }

    internal static Resource Rehydrate(
        ResourceId id,
        ResourceName name,
        WorkCalendarId workCalendarId,
        ResourceEfficiency efficiency,
        ResourcePerformance performance,
        ResourceCapacity capacity,
        ResourceTypeId resourceTypeId,
        ResourceCostRate costRate,
        CostRateMeasureUnits costRateMeasureUnitId,
        ResourceStatuses resourceStatusId,
        List<ResourceCapability>? capabilities)
    {
        // فراخوانی سازنده خصوصی
        return new Resource(
           id,
           name,
           workCalendarId,
           efficiency,
           performance,
           capacity,
           resourceTypeId,
           costRate, 
           costRateMeasureUnitId, 
           resourceStatusId,
           capabilities);
    }
}
