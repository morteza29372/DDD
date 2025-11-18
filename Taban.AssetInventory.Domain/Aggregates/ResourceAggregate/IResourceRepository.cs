using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate;

public interface IResourceRepository
{
    Task<Resource?> GetById(ResourceId id, CancellationToken cancellationToken = default);

    Task Add(Resource resource, CancellationToken cancellationToken);

    Task Update(Resource resource, long concurrencyStamp, CancellationToken cancellationToken = default);

    Task Remove(Resource resource, long concurrencyStamp, CancellationToken cancellationToken = default);

    Task<Resource?> CheckByName(ResourceName name, ResourceId id, CancellationToken cancellationToken = default);
}
