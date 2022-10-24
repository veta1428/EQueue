using Rey.EQueue.Shared;
using Rey.EQueue.Shared.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.EF.Repositories
{
    public class Repository<TEntity> : Repository<ApplicationDbContext, TEntity, int> where TEntity : Entity<int>
    {
        public Repository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
