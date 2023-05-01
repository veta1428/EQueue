using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Repositories
{
    public class RecordRepository : Repository<Record>, IRecordRepository
    {
        public RecordRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Record?> GetLastRecordByQueueIdAsync(int queueId, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(r => r.QueueId == queueId && r.NextRecordId == null)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Record?> GetNextRecordAsync(Record record, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(r => r.Id == record.NextRecordId)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Record?> GetPrevRecordAsync(Record record, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(r => r.NextRecordId == record.Id)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Record>> GetRecordsByQueueIdAsync(int queueId, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(r => r.QueueId == queueId)
                .Include(r => r.User)
                .Include(r => r.ChangeTo)
                .ToListAsync(cancellationToken);           
        }

        public async Task<Record?> GetUserRecordByQueueAsync(int userId, int queueId, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(r => r.UserId == userId && r.QueueId == queueId)
                .Include(r => r.ChangeFrom)
                .Include(r => r.ChangeTo)
                .SingleOrDefaultAsync(cancellationToken);

        }
    }
}
