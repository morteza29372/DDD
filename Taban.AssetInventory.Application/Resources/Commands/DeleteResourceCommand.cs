using FluentResults;
using MediatR;
using Taban.AssetInventory.Application.Common.Authorization;
using Taban.AssetInventory.Application.Common.Interfaces;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate;
using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Application.Resources.Commands;

public record DeleteResourceResponse(long Id, Result Result);

public record DeleteResourceCommand(
    long Id,
    long ConcurrencyStamp
);

public class DeleteResourcesCommand : IRequest<Result<List<DeleteResourceResponse>>>, IRequiresPermission
{
    public List<DeleteResourceCommand> Resources { get; set; } = new();

    public long[] RequiredPermissions => [ResourcePermissions.Delete];
}

public class DeleteResourceTypesCommandHandler(
    IResourceRepository repository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteResourcesCommand, Result<List<DeleteResourceResponse>>>
{
    public async Task<Result<List<DeleteResourceResponse>>> Handle(DeleteResourcesCommand request, CancellationToken cancellationToken)
    {
        var results = new List<DeleteResourceResponse>();

        foreach (var deleteResourceCommand in request.Resources)
        {
            try
            {
                var resourceType = await repository.GetById(deleteResourceCommand.Id, cancellationToken);

                if (resourceType is null)
                    throw new DomainException($"منبع با شناسه {deleteResourceCommand.Id} یافت نشد.");

                await repository.Remove(resourceType, deleteResourceCommand.ConcurrencyStamp, cancellationToken);

                results.Add(new DeleteResourceResponse(deleteResourceCommand.Id, Result.Ok()));
            }
            catch (DomainException e)
            {
                results.Add(new DeleteResourceResponse(deleteResourceCommand.Id, Result.Fail(e.Message)));
            }
        }

        if (results.Any(x => x.Result.IsFailed))
        {
            return Result.Fail<List<DeleteResourceResponse>>(results.SelectMany(x => x.Result.Errors));
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(results).WithSuccess($"{request.Resources.Count} منبع با موفقیت حذف شد.");
    }
}
