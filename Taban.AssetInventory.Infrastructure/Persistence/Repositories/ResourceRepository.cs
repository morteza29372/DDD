using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taban.AssetInventory.DataLayer.Context;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate;
using Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;
using Taban.AssetInventory.Infrastructure.Persistence.Common;
using Taban.AssetInventory.Infrastructure.Persistence.EfModels;
using Taban.AssetInventory.Infrastructure.Persistence.Mappers;

namespace Taban.AssetInventory.Infrastructure.Persistence.Repositories;

public class ResourceRepository(
    AssetInventoryDbContext dbContext
    ) : IResourceRepository
{
    public async Task<Resource> GetById(ResourceId id, CancellationToken cancellationToken = default)
    {
        var dbModel = await dbContext.Set<DbResource>()
            .WhereNotDeleted()
            .Include(db=>db.Capabilities)
            .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (dbModel is null)
        {
            return null;
        }

        var model = dbModel.ToDomainModel();

        return model;
    }

    public async Task Add(Resource resource, CancellationToken cancellationToken)
    {
        var dbModel = resource.ToDbModel();

        await dbContext.Set<DbResource>().AddAsync(dbModel, cancellationToken);
    }

    public async Task Update(Resource resource, long concurrencyStamp, CancellationToken cancellationToken = default)
    {
        var dbModel = await dbContext.Set<DbResource>()
            .Include(r => r.Capabilities)
            .SingleOrDefaultAsync(e => e.Id == resource.Id, cancellationToken);

        var updatedEntity = resource.ToDbModel();

        dbModel.Name = updatedEntity.Name;
        dbModel.Capacity = updatedEntity.Capacity;
        dbModel.CostRate = updatedEntity.CostRate;
        dbModel.CostRateMeasureUnitId = updatedEntity.CostRateMeasureUnitId;
        dbModel.Efficiency = updatedEntity.Efficiency;
        dbModel.Performance = updatedEntity.Performance;
        dbModel.ResourceStatusId = updatedEntity.ResourceStatusId;
        dbModel.ResourceTypeId = updatedEntity.ResourceTypeId;
        dbModel.WorkCalendarId = updatedEntity.WorkCalendarId;

        var capabilities = updatedEntity.Capabilities?
            .Select(capability => new DbResourceCapability()
            {
                Id = capability.Id,
                CapabilityId = capability.CapabilityId,
                ResourceId = capability.ResourceId
            })
            .ToList() ?? new List<DbResourceCapability>();

        dbContext.UpdateList(capabilities, dbModel.Capabilities!.ToList());

        dbContext.Entry(dbModel).Property(e => e.ConcurrencyStamp).OriginalValue = concurrencyStamp;
    }

    public async Task Remove(Resource resource, long concurrencyStamp, CancellationToken cancellationToken = default)
    {
        var dbModel = await dbContext.Set<DbResource>()
            .Include(e => e.Capabilities)
            .SingleAsync(e => e.Id == resource.Id, cancellationToken);

        dbContext.Entry(dbModel).Property(e => e.ConcurrencyStamp).OriginalValue = concurrencyStamp;

        if (dbModel.Capabilities.Any())
        {
            dbContext.Set<DbResourceCapability>().RemoveRange(dbModel.Capabilities);
        }

        dbContext.Set<DbResource>().Remove(dbModel);
    }

    public async Task<Resource> CheckByName(ResourceName name, ResourceId id, CancellationToken cancellationToken = default)
    {
        var dbModel = await dbContext.Set<DbResource>()
            .WhereNotDeleted()
            .FirstOrDefaultAsync(e => e.Name == name && e.Id != id, cancellationToken);

        if (dbModel is null)
        {
            return null;
        }

        var model = dbModel.ToDomainModel();

        return model;
    }
}
