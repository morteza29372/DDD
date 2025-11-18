using Taban.AssetInventory.Domain.Aggregates.CapabilityAggregate.ValueObjects;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate;

public class ResourceCapability
{

    public ResourceCapability(
        ResourceCapabilityId id,
        ResourceId resourceId,
        CapabilityId capabilityId
        )
    {
        Id= id;
        ResourceId= resourceId;
        CapabilityId= capabilityId;
    }


    public ResourceCapabilityId Id { get; private set; }

    public ResourceId ResourceId { get; private set; }

    public CapabilityId CapabilityId { get; private set; }


    internal static ResourceCapability Rehydrate(
        ResourceCapabilityId id,
        ResourceId resourceId,
        CapabilityId capabilityId
        )
    {
        // فراخوانی سازنده خصوصی
        return new ResourceCapability(
            id,
            resourceId, 
            capabilityId);
    }
}
