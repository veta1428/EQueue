using Rey.EQueue.Core.Entities;
using Rey.EQueue.Shared.Interfaces;

namespace Rey.EQueue.Application.Repositories
{
    public interface IQueueRepository: IRepository<Queue, int>
    {
        Task<Queue> GetQueueByIdDetailedAsync(int queueId, CancellationToken cancellationToken);
    }
}
