using Rey.EQueue.Core.Entities;
using Rey.EQueue.Shared.Interfaces;

namespace Rey.EQueue.Application.Repositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> GetUserByIdWithQueuesAndRecordsAsync(int userId, CancellationToken cancellationToken);

        Task<User> GetSystemUserAsync(CancellationToken cancellationToken);
    }
}
