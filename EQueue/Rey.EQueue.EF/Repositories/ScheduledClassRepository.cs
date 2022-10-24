using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Repositories
{
    public class ScheduledClassRepository : Repository<ScheduledClass>, IScheduledClassRepository
    {
        public ScheduledClassRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
