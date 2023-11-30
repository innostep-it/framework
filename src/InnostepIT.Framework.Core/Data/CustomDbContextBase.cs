using InnostepIT.Framework.Core.Contract.Data;
using InnostepIT.Framework.Core.Contract.FrameworkAdapter;
using InnostepIT.Framework.Core.Contract.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace InnostepIT.Framework.Core.Data;

public abstract class CustomDbContextBase : DbContext, ICustomDbContext
{
    private readonly IDateTimeAdapter _dateTimeAdapter;
    private readonly IIdentityStore _identityStore;
    private readonly ILogger<CustomDbContextBase> _logger;

    protected CustomDbContextBase(ILogger<CustomDbContextBase> logger, IDateTimeAdapter dateTimeAdapter,
        IIdentityStore identityStore)
    {
        _logger = logger;
        _dateTimeAdapter = dateTimeAdapter;
        _identityStore = identityStore;
    }

    public new TEntity Add<TEntity>(TEntity entity) where TEntity : class
    {
        TEntity entry;
        lock (this)
        {
            _logger.LogDebug("Adding entity of type {TypeName}", entity.GetType().Name);
            if (entity is ChangeTrackedEntity trackedEntity)
                UpdateChangeTrackingFields(trackedEntity, true);

            entry = base.Add(entity).Entity;
        }

        return entry;
    }

    public new async Task<TEntity?> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await Task.Run(() =>
        {
            _logger.LogDebug("Adding entity of type {TypeName} async ...", entity.GetType().Name);

            EntityEntry entry;
            lock (this)
            {
                if (entity is ChangeTrackedEntity trackedEntity)
                    UpdateChangeTrackingFields(trackedEntity, true);

                entry = base.Add(entity);
            }

            _logger.LogDebug("Finished adding entity of type {TypeName} async. ", entity.GetType().Name);

            return entry.Entity as TEntity;
        }, cancellationToken);
    }

    public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        lock (this)
        {
            _logger.LogDebug("Adding range of entities of type {TypeName}", entities.GetType().Name);

            if (entities is IEnumerable<ChangeTrackedEntity> trackedEntities)
                foreach (var trackedEntity in trackedEntities)
                    UpdateChangeTrackingFields(trackedEntity, true);

            base.AddRange(entities);
        }
    }

    public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        await Task.Run(() =>
        {
            lock (this)
            {
                _logger.LogDebug("Adding range of entities of type {TypeName} async", entities.GetType().Name);

                if (entities is IEnumerable<ChangeTrackedEntity> trackedEntities)
                    foreach (var trackedEntity in trackedEntities)
                        UpdateChangeTrackingFields(trackedEntity, true);

                base.AddRangeAsync(entities, cancellationToken);
            }
        }, cancellationToken);
    }

    public new TEntity Update<TEntity>(TEntity entity) where TEntity : class
    {
        TEntity entry;
        lock (this)
        {
            _logger.LogDebug("Updating entity of type {TypeName} ...", entity.GetType().Name);
            if (entity is ChangeTrackedEntity trackedEntity)
                UpdateChangeTrackingFields(trackedEntity);

            entry = base.Update(entity).Entity;
        }

        return entry;
    }

    public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        lock (this)
        {
            _logger.LogDebug("updating range of entities of type {TypeName}", entities.GetType().Name);

            if (entities is IEnumerable<ChangeTrackedEntity> trackedEntities)
                foreach (var trackedEntity in trackedEntities)
                    UpdateChangeTrackingFields(trackedEntity);

            base.UpdateRange(entities);
        }
    }

    public override int SaveChanges()
    {
        lock (this)
        {
            _logger.LogDebug("saving changes...");
            return base.SaveChanges();
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            lock (this)
            {
                _logger.LogDebug("saving changes async...");
                return base.SaveChangesAsync(cancellationToken);
            }
        }, cancellationToken);
    }

    private void UpdateChangeTrackingFields(ChangeTrackedEntity changeTrackedEntity, bool newEntity = false)
    {
        if (newEntity)
        {
            changeTrackedEntity.CreatedAt = _dateTimeAdapter.GetUtcDateTime();
            changeTrackedEntity.CreatedBy = _identityStore.GetCurrentUser();
        }

        changeTrackedEntity.LastChangedAt = _dateTimeAdapter.GetUtcDateTime();
        changeTrackedEntity.LastChangedBy = _identityStore.GetCurrentUser();
    }
}