using Taban.AssetInventory.Domain.Aggregates.CapabilityAggregate;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.Services;
using Taban.AssetInventory.Domain.Aggregates.ResourceTypeAggregate;
using Taban.AssetInventory.Domain.Aggregates.WorkCalendarAggregate;
using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Application.Resources.Services;

public class ResourceValidatorService(
    ResourceUniquenessChecker validatorService,
    IResourceTypeRepository resourceTypeRepository,
    IWorkCalendarRepository workCalendarRepository,
    ICapabilityRepository capabilityRepository
    )
{
    public async Task ValidateSharedRulesAsync(
        Resource resource,
        CancellationToken cancellationToken = default)
    {
        if (resource.ResourceStatusId == Domain.Aggregates.ResourceAggregate.Enums.ResourceStatuses.Breakdown)
            throw new DomainException("منبع نمیتواند در وضعیت خراب باشد.");

        await validatorService.CheckAsync(resource.Name, resource.Id, cancellationToken);

        var resourceType = await resourceTypeRepository.GetById(resource.ResourceTypeId, cancellationToken);

        if (resourceType == null)
            throw new DomainException($"نوع منبع با شناسه {resource.ResourceTypeId.Value} یافت نشد.");

        if (!resourceType.IsActive)
            throw new DomainException($"نوع منبع {resourceType.Name} در وضعیت فعال نمی‌باشد.");

        var workCalendar = await workCalendarRepository.GetById(resource.WorkCalendarId, cancellationToken);

        if (workCalendar == null)
            throw new DomainException($"تقویم کاری با شناسه {resource.WorkCalendarId.Value} یافت نشد");

        foreach (var resourceCapability in resource.ResourceCapabilities)
        {
            var capability = await capabilityRepository.GetById(resourceCapability.CapabilityId, cancellationToken);

            if (capability is null)
                throw new DomainException($"قابلیت با شناسه {resourceCapability.CapabilityId.Value} یافت نشد.");

            if (!capability.IsActive)
                throw new DomainException($"قابلیت {capability.Name} در وضعیت فعال نمی‌باشد.");
        }
    }

}
