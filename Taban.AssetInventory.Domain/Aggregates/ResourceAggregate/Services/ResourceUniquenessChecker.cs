using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;
using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.Services;

public class ResourceUniquenessChecker(IResourceRepository resourceRepository)
{
    public async Task CheckAsync(
        ResourceName name,
        ResourceId id,
        CancellationToken cancellationToken = default
        )
    {
        var resourceByName = await resourceRepository.CheckByName(name, id, cancellationToken);

        if (resourceByName != null)
            throw new DomainException($"نام {name.Value} تکراری است.");
    }
}
