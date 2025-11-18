using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gridify;
using Microsoft.EntityFrameworkCore;
using Taban.AssetInventory.Application.Resources.Dtos;
using Taban.AssetInventory.Application.Resources.Interfaces;
using Taban.AssetInventory.DataLayer.Context;
using Taban.AssetInventory.Domain.Common;
using Taban.AssetInventory.Infrastructure.DataGrid;
using Taban.AssetInventory.Infrastructure.Persistence.Common;
using Taban.AssetInventory.Infrastructure.Persistence.EfModels;

namespace Taban.AssetInventory.Infrastructure.Persistence.Queries;

public class ResourceQueryService(
    AssetInventoryDbContext dbContext
    ) :IResourceQueryService
{
    public async Task<GetResourceDto> Get(long resourceId, CancellationToken cancellationToken = default)
    {
        var dbEntity = await dbContext.Set<DbResource>()
            .Where(entity => entity.Id == resourceId)
            .WhereNotDeleted()
            .Select(entity => new GetResourceDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                ConcurrencyStamp = entity.ConcurrencyStamp,
                Capacity = entity.Capacity,
                CapacityMeasureUnitName = entity.ResourceType.CapacityMeasureUnit.Name,
                CostRate = entity.CostRate,
                CostRateMeasureUnitId =(int) entity.CostRateMeasureUnitId,
                CostRateMeasureUnitName = entity.CostRateMeasureUnit.Name,
                Efficiency = entity.Efficiency,
                Performance = entity.Performance,
                ResourceStatusId =(int) entity.ResourceStatusId,
                ResourceStatusName = entity.ResourceStatus.Name,
                ResourceTypeId = entity.ResourceTypeId,
                ResourceTypeName = entity.ResourceType.Name,
                WorkCalendarId = entity.WorkCalendarId,
                WorkCalendarName = entity.WorkCalendar.Name,
                Capabilities = entity.Capabilities.Select(c=>c.CapabilityId).ToList()
            }).SingleOrDefaultAsync(cancellationToken);

        if (dbEntity is null)
        {
            throw new DomainException($"منبع با شناسه {resourceId} یافت نشد.");
        }

        return dbEntity;
    }

    public async Task<DataGridResult<GetResourceDataTableDto>> GetDataTable(DataGridRequest gridRequest, CancellationToken cancellationToken = default)
    {
        var mapper = new GridifyMapper<DbResource>()
            .GenerateMappings(2)
            .AddMap("resourceStatusId",i=>(int) i.ResourceStatusId)
            .AddMap("capacityMeasureUnitId", i=>(int) i.ResourceType.CapacityMeasureUnitId)
            .AddMap("costRateMeasureUnitId", i=>(int) i.CostRateMeasureUnitId);

        return await dbContext.Set<DbResource>()
            .WhereNotDeleted()
            .ToDataGrid(
                gridRequest,
                mapper,
                items => items
                    .Select(entity => new GetResourceDataTableDto()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        ConcurrencyStamp = entity.ConcurrencyStamp,
                        Capacity = entity.Capacity,
                        CapacityMeasureUnitName = entity.ResourceType.CapacityMeasureUnit.Name,
                        CostRate = entity.CostRate,
                        CostRateMeasureUnitId = entity.CostRateMeasureUnitId,
                        CostRateMeasureUnitName = entity.CostRateMeasureUnit.Name,
                        Efficiency = entity.Efficiency,
                        Performance = entity.Performance,
                        ResourceStatusId = entity.ResourceStatusId,
                        ResourceStatusName = entity.ResourceStatus.Name,
                        ResourceTypeId = entity.ResourceTypeId,
                        ResourceTypeName = entity.ResourceType.Name,
                        WorkCalendarId = entity.WorkCalendarId,
                        WorkCalendarName = entity.WorkCalendar.Name
                    }),
                cancellationToken);
    }

    public async Task<List<GetResourceSelectItemsDto>> GetSelectItems(CancellationToken cancellationToken = default)
    {
        var dbEntity = await dbContext.Set<DbResource>()
            .WhereNotDeleted()
            .Select(entity => new GetResourceSelectItemsDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ConcurrencyStamp = entity.ConcurrencyStamp,
                Capacity = entity.Capacity,
                CapacityMeasureUnitName = entity.ResourceType.CapacityMeasureUnit.Name,
                CostRate = entity.CostRate,
                CostRateMeasureUnitId = entity.CostRateMeasureUnitId,
                CostRateMeasureUnitName = entity.CostRateMeasureUnit.Name,
                Efficiency = entity.Efficiency,
                Performance = entity.Performance,
                ResourceStatusId = entity.ResourceStatusId,
                ResourceStatusName = entity.ResourceStatus.Name,
                ResourceTypeId = entity.ResourceTypeId,
                ResourceTypeName = entity.ResourceType.Name,
                WorkCalendarId = entity.WorkCalendarId,
                WorkCalendarName = entity.WorkCalendar.Name
            })
            .ToListAsync(cancellationToken);

        return dbEntity;
    }
}
