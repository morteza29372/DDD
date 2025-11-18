using Taban.AssetInventory.Application.Resources.Dtos;
using Taban.AssetInventory.Infrastructure.DataGrid;

namespace Taban.AssetInventory.Application.Resources.Interfaces;

public interface IResourceQueryService
{
    Task<GetResourceDto> Get(
        long resourceId,
        CancellationToken cancellationToken = default
    );
    
    Task<DataGridResult<GetResourceDataTableDto>> GetDataTable(
        DataGridRequest gridRequest,
        CancellationToken cancellationToken = default
    );

    Task<List<GetResourceSelectItemsDto>> GetSelectItems(
        CancellationToken cancellationToken = default
    );
}
