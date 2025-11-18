using MediatR;
using Taban.AssetInventory.Application.Common.Authorization;
using Taban.AssetInventory.Application.Resources.Dtos;
using Taban.AssetInventory.Application.Resources.Interfaces;
using Taban.AssetInventory.Infrastructure.DataGrid;

namespace Taban.AssetInventory.Application.Resources.Queries;

public record GetResourceDataTableQuery(DataGridRequest GridRequest) : IRequest<DataGridResult<GetResourceDataTableDto>>, IRequiresPermission
{
    public long[] RequiredPermissions => [ResourcePermissions.View];
}

public class GetResourceDataTableQueryHandler(
    IResourceQueryService queryService
    )
    : IRequestHandler<GetResourceDataTableQuery, DataGridResult<GetResourceDataTableDto>>
{
    public async Task<DataGridResult<GetResourceDataTableDto>> Handle(GetResourceDataTableQuery request, CancellationToken cancellationToken)
    {
        return await queryService.GetDataTable(request.GridRequest, cancellationToken);
    }
}
