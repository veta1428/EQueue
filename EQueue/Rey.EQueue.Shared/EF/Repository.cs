using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Shared.Exceptions;
using Rey.EQueue.Shared.Interfaces;

namespace Rey.EQueue.Shared.EF
{
    public abstract class BaseRepository<TDbContext, TDbEntity, TId>
            where TDbContext : DbContext
            where TDbEntity : Entity<TId>
            where TId : struct, IEquatable<TId>
    {
        private protected BaseRepository(TDbContext context)
        {
            Context = context;
            DbSet = context.Set<TDbEntity>();
        }

        protected TDbContext Context { get; }

        protected DbSet<TDbEntity> DbSet { get; }

        public virtual bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            if (HasChanges())
                await Context.SaveChangesAsync(cancellationToken);
        }

        protected virtual IQueryable<TDbEntity> GetQuery()
        {
            return DbSet;
        }
    }

    public abstract class Repository<TDbContext, TEntity, TId> : BaseRepository<TDbContext, TEntity, TId>, IRepository<TEntity, TId>
        where TDbContext : DbContext
        where TEntity : Entity<TId>
        where TId : struct, IEquatable<TId>
    {
        protected Repository(TDbContext context)
            : base(context)
        {
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity).DetectChanges();
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
            Context.ChangeTracker.DetectChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity).DetectChanges();
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            Context.ChangeTracker.DetectChanges();
        }

        public virtual void Update(TEntity entity)
        {
            var entry = Context.Entry(entity);

            if (entry.State != EntityState.Added && entry.State != EntityState.Modified)
            {
                DbSet.Update(entity).DetectChanges();
            }
            else
            {
                entry.DetectChanges();
            }
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public virtual async Task<TEntity> FindByIdAsync(TId id, CancellationToken cancellationToken)
        {
            return await GetQuery().SingleOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken)
                           ?? throw new NotFoundException(typeof(TEntity).Name);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken)
        {
            return await GetQuery().ToArrayAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<TId> ids, CancellationToken cancellationToken)
        {
            return await GetQuery().Where(e => ids.Contains(e.Id)).ToArrayAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> TryFindByIdAsync(TId id, CancellationToken cancellationToken)
        {
            return await GetQuery().SingleOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);
        }
    }
}
