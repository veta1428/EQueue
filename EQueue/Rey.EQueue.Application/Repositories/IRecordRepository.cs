using Rey.EQueue.Core.Entities;
using Rey.EQueue.Shared.Interfaces;

namespace Rey.EQueue.Application.Repositories
{
    public interface IRecordRepository: IRepository<Record, int>
    {
        Task<List<Record>> GetRecordsByQueueIdAsync(int queueId, CancellationToken cancellationToken);

        Task<Record?> GetLastRecordByQueueIdAsync(int queueId, CancellationToken cancellationToken);

        Task<Record?> GetPrevRecordAsync(Record record, CancellationToken cancellationToken);

        Task<Record?> GetNextRecordAsync(Record record, CancellationToken cancellationToken);

        Task<Record?> GetUserRecordByQueueAsync(int userId, int queueId, CancellationToken cancellationToken);
    }
}
