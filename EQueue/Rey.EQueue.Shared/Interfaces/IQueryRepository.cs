using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Shared.Interfaces
{
    public interface IQueryRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct, IEquatable<TId>
    {
        Task<TEntity?> TryFindByIdAsync(TId id, CancellationToken cancellationToken);

        Task<TEntity> FindByIdAsync(TId id, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<TId> ids, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken);

        IQueryable<TEntity> GetQuery();
    }
}
