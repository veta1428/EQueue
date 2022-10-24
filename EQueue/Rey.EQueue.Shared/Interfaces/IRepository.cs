using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Shared.Interfaces
{
    public interface IRepository<TEntity, TId> 
        : IChangeRepository<TEntity, TId>, IQueryRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct, IEquatable<TId>
    {
    }

    public interface IRepository<TEntity>
        : IChangeRepository<TEntity, int>, IQueryRepository<TEntity, int>
        where TEntity : Entity<int>
    {
    }
}
