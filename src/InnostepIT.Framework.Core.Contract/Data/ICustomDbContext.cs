namespace InnostepIT.Framework.Core.Contract.Data
{
    public interface ICustomDbContext : IDisposable
    {
        TEntity Add<TEntity>(TEntity entity) where TEntity : class;
        Task<TEntity?> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;
        TEntity Update<TEntity>(TEntity entity) where TEntity : class;
        void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
