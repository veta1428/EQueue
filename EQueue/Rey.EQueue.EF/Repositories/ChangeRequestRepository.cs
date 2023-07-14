using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Repositories
{
    public class ChangeRequestRepository: Repository<ChangeRequest>, IChangeRequestRepository
    {
        public ChangeRequestRepository(ApplicationDbContext context) : base(context)
        {     
        }

        public async Task<IEnumerable<ChangeRequest>> GetUserIncomingsAsync(int userId, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(chr => chr.UserToId == userId)
                .Include(chr => chr.RecordFrom)
                .Include(chr => chr.RecordTo)
                .Include(chr => chr.UserFrom)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ChangeRequest>> GetUserOutcomingsAsync(int userId, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(chr => chr.UserFromId == userId)
                .Include(chr => chr.RecordFrom)
                .Include(chr => chr.RecordTo)
                .Include(chr => chr.UserTo)
                .ToListAsync(cancellationToken);
        }
    }
}
