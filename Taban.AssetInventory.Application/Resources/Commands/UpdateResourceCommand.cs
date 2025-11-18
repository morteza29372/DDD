using FluentResults;
using MediatR;
using Taban.AssetInventory.Application.Common.Authorization;
using Taban.AssetInventory.Application.Common.Interfaces;
using Taban.AssetInventory.Application.Resources.Services;
using Taban.AssetInventory.Domain.Aggregates.CapabilityAggregate.ValueObjects;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;
using Taban.AssetInventory.Domain.Aggregates.ResourceTypeAggregate.ValueObjects;
using Taban.AssetInventory.Domain.Aggregates.WorkCalendarAggregate.ValueObjects;
using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Application.Resources.Commands;

public record UpdateResourceResponse(long Id);

public class UpdateResourceCommand : IRequest<Result<UpdateResourceResponse>>, IRequiresPermission
{
    public long Id { get; set; }

    public long ConcurrencyStamp { get; set; }

    public string Name { get; set; } = string.Empty;

    public int WorkCalendarId { get; set; }

    public decimal Efficiency { get; set; }

    public decimal Performance { get; set; }

    public decimal Capacity { get; set; }

    public long ResourceTypeId { get; set; }

    public decimal CostRate { get; set; }

    public Domain.Aggregates.ResourceAggregate.Enums.CostRateMeasureUnits CostRateMeasureUnitId { get; set; }

    public Domain.Aggregates.ResourceAggregate.Enums.ResourceStatuses ResourceStatusId { get; set; }

    public List<long> Capabilities { get; set; } = new();

    public long[] RequiredPermissions => [ResourcePermissions.Update];
}

public class UpdateResourceCommandHandler(
    IResourceRepository repository,
    ResourceValidatorService validatorService,
    IUnitOfWork unitOfWork,
    IIdGenerator idGenerator
    )
    : IRequestHandler<UpdateResourceCommand, Result<UpdateResourceResponse>>
{
    public async Task<Result<UpdateResourceResponse>> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var id = new ResourceId(request.Id);
            var resource = await repository.GetById(id, cancellationToken);

            if (resource is null)
                throw new DomainException($"منبع با شناسه {request.Id} یافت نشد.");

            var name = new ResourceName(request.Name);
            resource.UpdateName(name);

            var workCalendarId = new WorkCalendarId(request.WorkCalendarId);
            resource.UpdateWorkCalendarId(workCalendarId);

            var efficiency = new ResourceEfficiency(request.Efficiency);
            resource.UpdateEfficiency(efficiency); 

            var performance = new ResourcePerformance(request.Performance);
            resource.UpdatePerformance(performance);

            var capacity = new ResourceCapacity(request.Capacity);
            resource.UpdateCapacity(capacity);

            var resourceTypeId = new ResourceTypeId(request.ResourceTypeId);
            resource.UpdateResourceTypeId(resourceTypeId);

            var costRate = new ResourceCostRate(request.CostRate);
            resource.UpdateCostRate(costRate);

            resource.UpdateCostRateMeasureUnitId(request.CostRateMeasureUnitId);
            resource.UpdateResourceStatusId(request.ResourceStatusId);

            var resourceCapabilityToRemove = resource.ResourceCapabilities
                .Where(d => request.Capabilities.All(ic => ic != d.CapabilityId))
                .ToList();

            foreach (var resourceCapability in resourceCapabilityToRemove)
            {
                resource.RemoveResourceCapability(resourceCapability.Id);
            }

            foreach (var capabilityId in request.Capabilities)
            {
                var capability = resource.ResourceCapabilities.SingleOrDefault(c => c.CapabilityId == capabilityId);

                if (capability is null)
                {
                    resource.AddResourceCapability(new ResourceCapability(
                        new ResourceCapabilityId(idGenerator.NewId()),
                        new ResourceId(id),
                        new CapabilityId(capabilityId)
                        ));
                }
            }

            await validatorService.ValidateSharedRulesAsync(resource, cancellationToken);

            await repository.Update(resource, request.ConcurrencyStamp, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok(new UpdateResourceResponse(id.Value)).WithSuccess("منبع با موفقیت ویرایش شد.");
        }
        catch (DomainException ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
