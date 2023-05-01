using Rey.EQueue.Core.Entities;
using Rey.EQueue.Shared.Interfaces;

namespace Rey.EQueue.Application.Repositories
{
    public interface IChangeRequestRepository : IRepository<ChangeRequest, int>
    {
        Task<IEnumerable<ChangeRequest>> GetUserOutcomingsAsync(int userId, CancellationToken cancellationToken);

        Task<IEnumerable<ChangeRequest>> GetUserIncomingsAsync(int userId, CancellationToken cancellationToken);
    }
}
