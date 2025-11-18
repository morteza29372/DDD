using FluentResults;
using MediatR;
using Taban.AssetInventory.Application.Common.Authorization;
using Taban.AssetInventory.Application.Resources.Dtos;
using Taban.AssetInventory.Application.Resources.Interfaces;
using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Application.Resources.Queries;


public record GetResourceQuery(long Id) : IRequest<Result<GetResourceDto>>, IRequiresPermission
{
    public long[] RequiredPermissions => [ResourcePermissions.Update];
}

public class GetResourceQueryHandler(
    IResourceQueryService queryService
    ) : IRequestHandler<GetResourceQuery, Result<GetResourceDto>>
{
    public async Task<Result<GetResourceDto>> Handle(GetResourceQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await queryService.Get(request.Id, cancellationToken);
        }
        catch (DomainException ex)
        {
           return Result.Fail(ex.Message);
        }
    }
}
