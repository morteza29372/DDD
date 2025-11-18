#nullable enable
using System.Linq;
using Taban.AssetInventory.Domain.Aggregates.CapabilityAggregate.ValueObjects;
using Taban.AssetInventory.Infrastructure.Persistence.EfModels;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;
using Taban.AssetInventory.Domain.Aggregates.ResourceTypeAggregate.ValueObjects;
using Taban.AssetInventory.Domain.Aggregates.WorkCalendarAggregate.ValueObjects;

namespace Taban.AssetInventory.Infrastructure.Persistence.Mappers;

public static class ResourceMapper
{
    public static Resource ToDomainModel(this DbResource dbResource)
    {
        var capabilities = dbResource.Capabilities?
            .Select(d => new ResourceCapability(
                new ResourceCapabilityId(d.Id),
                new ResourceId(d.ResourceId),
                new CapabilityId(d.CapabilityId)
                ))
            .ToList();

        // فراخوانی متد استاتیک عمومی به جای سازنده خصوصی
        return Resource.Rehydrate(
            new ResourceId(dbResource.Id),
            new ResourceName(dbResource.Name),
            new WorkCalendarId(dbResource.WorkCalendarId),
            new ResourceEfficiency(dbResource.Efficiency),
            new ResourcePerformance(dbResource.Performance),
            new ResourceCapacity(dbResource.Capacity),
            new ResourceTypeId(dbResource.ResourceTypeId),
            new ResourceCostRate(dbResource.CostRate),
            dbResource.CostRateMeasureUnitId,
            dbResource.ResourceStatusId,
            capabilities
            );
    }

    public static DbResource ToDbModel(this Resource resource)
    {
        var dbModel = new DbResource
        {
            Id = resource.Id.Value,
            Name = resource.Name.Value,
            ResourceTypeId = resource.ResourceTypeId.Value,
            Capacity = resource.Capacity.Value,
            CostRate = resource.CostRate.Value,
            CostRateMeasureUnitId = resource.CostRateMeasureUnitId,
            Efficiency = resource.Efficiency.Value,
            Performance = resource.Performance.Value,
            ResourceStatusId = resource.ResourceStatusId,
            WorkCalendarId = resource.WorkCalendarId.Value,
            Capabilities = resource.ResourceCapabilities.Select(resourceCapability => new DbResourceCapability
            {
                Id = resourceCapability.Id.Value,
                CapabilityId = resourceCapability.CapabilityId.Value,
                ResourceId = resourceCapability.ResourceId.Value
            }).ToList()
        };

        return dbModel;
    }

    public static ResourceCapability ToDomainModel(this DbResourceCapability dbResourceCapability)
    {
        return ResourceCapability.Rehydrate(
            new ResourceCapabilityId(dbResourceCapability.Id),
            new ResourceId(dbResourceCapability.ResourceId),
            new CapabilityId(dbResourceCapability.CapabilityId)
            );
    }

    public static DbResourceCapability ToDbModel(this ResourceCapability resourceCapability)
    {
        var dbModel = new DbResourceCapability
        {
            Id = resourceCapability.Id.Value,
            CapabilityId = resourceCapability.CapabilityId.Value,
            ResourceId = resourceCapability.ResourceId.Value
        };

        return dbModel;
    }
}
