using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Shared
{
    public abstract class Entity<TId> 
        where TId : struct, IEquatable<TId>
    {
        public TId Id { get; set; }
    }

    public abstract class Entity : Entity<int>
    {
    }
}
