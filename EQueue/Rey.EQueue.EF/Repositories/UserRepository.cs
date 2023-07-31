using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetSystemUserAsync(CancellationToken cancellationToken)
        {
            return await Context.Users
                .Where(user => user.Id == 1)
                .Include(user => user.Roles)
                .ThenInclude(ur => ur.Role)
                .SingleAsync(cancellationToken);
        }

        public async Task<User> GetUserByIdWithQueuesAndRecordsAsync(int userId, CancellationToken cancellationToken)
        {
            return await Context.Users
                .Where(user => user.Id == userId)
                .Include(u => u.Records)
                    .ThenInclude(r => r.ChangeFrom)
                .Include(u => u.Records)
                    .ThenInclude(r => r.Queue)
                        .ThenInclude(q => q.Records)
                .Include(u => u.Records)
                    .ThenInclude(r => r.ChangeTo)
                .Include(u => u.Records)
                    .ThenInclude(r => r.Queue)
                .SingleAsync(cancellationToken);
        }
    }
}
