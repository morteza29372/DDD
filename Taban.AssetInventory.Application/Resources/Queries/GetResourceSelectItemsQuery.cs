using MediatR;
using Taban.AssetInventory.Application.Resources.Dtos;
using Taban.AssetInventory.Application.Resources.Interfaces;

namespace Taban.AssetInventory.Application.Resources.Queries;

public record GetResourceSelectItemsQuery:IRequest<List<GetResourceSelectItemsDto>> {}

public class GetResourceSelectItemsQueryHandler(
    IResourceQueryService queryService
    ) : IRequestHandler<GetResourceSelectItemsQuery, List<GetResourceSelectItemsDto>>
{
    public async Task<List<GetResourceSelectItemsDto>> Handle(GetResourceSelectItemsQuery request, CancellationToken cancellationToken)
    {
        return await queryService.GetSelectItems(cancellationToken);
    }
}
