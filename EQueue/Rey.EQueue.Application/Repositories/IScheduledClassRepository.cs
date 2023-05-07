using Rey.EQueue.Core.Entities;
using Rey.EQueue.Shared.Interfaces;

namespace Rey.EQueue.Application.Repositories
{
    public interface IScheduledClassRepository: IRepository<ScheduledClass, int>
    {
        Task<ScheduledClass?> TryGetBySiIdAndStartTimeAsync(int siid, DateTime time, CancellationToken cancellationToken);
    }
}
