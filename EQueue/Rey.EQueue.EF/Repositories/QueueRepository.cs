using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Repositories
{
    public class QueueRepository : Repository<Queue>, IQueueRepository
    {
        public QueueRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Queue> GetQueueByIdDetailedAsync(int queueId, CancellationToken cancellationToken)
        {
            return await Context.Queues
                .Where(q => q.Id == queueId)
                .Include(q => q.Records)
                .Include(q => q.ScheduledClass)
                .ThenInclude(s => s.SubjectInstance)
                .SingleAsync(cancellationToken);
        }
    }
}
