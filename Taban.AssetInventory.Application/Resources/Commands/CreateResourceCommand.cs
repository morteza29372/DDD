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

public record CreateResourceResponse(long Id);

public class CreateResourceCommand : IRequest<Result<CreateResourceResponse>>, IRequiresPermission
{
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

    public long[] RequiredPermissions => [ResourcePermissions.Create];
}

public class CreateResourceCommandHandler(
    IResourceRepository repository,
    ResourceValidatorService  validatorService,
    IUnitOfWork unitOfWork,
    IIdGenerator idGenerator
    ) 
    : IRequestHandler<CreateResourceCommand, Result<CreateResourceResponse>>
{
    public async Task<Result<CreateResourceResponse>> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var id = new ResourceId(idGenerator.NewId());
            var name = new ResourceName(request.Name);
            var workCalendarId = new WorkCalendarId(request.WorkCalendarId);
            var efficiency = new ResourceEfficiency(request.Efficiency);
            var performance = new ResourcePerformance(request.Performance);
            var capacity = new ResourceCapacity(request.Capacity);
            var resourceTypeId = new ResourceTypeId(request.ResourceTypeId);
            var costRate = new ResourceCostRate(request.CostRate);

            var capabilities = new List<ResourceCapability>();

            foreach (var capabilityId in request.Capabilities)
            {
                capabilities.Add(new ResourceCapability(
                    new ResourceCapabilityId(idGenerator.NewId()),
                    id,
                    new CapabilityId(capabilityId)
                    ));
            }

            var resource = new Resource(
                id,
                name,
                workCalendarId,
                efficiency,
                performance,
                capacity,
                resourceTypeId,
                costRate,
                request.CostRateMeasureUnitId,
                request.ResourceStatusId,
                capabilities
            );

            await validatorService.ValidateSharedRulesAsync(resource, cancellationToken);

            await repository.Add(resource, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok(new CreateResourceResponse(id.Value)).WithSuccess("منبع با موفقیت ایجاد شد.");
        }
        catch (DomainException ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
