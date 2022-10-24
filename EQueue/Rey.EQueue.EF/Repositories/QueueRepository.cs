using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Repositories
{
    public class QueueRepository : Repository<Queue>, IQueueRepository
    {
        public QueueRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
