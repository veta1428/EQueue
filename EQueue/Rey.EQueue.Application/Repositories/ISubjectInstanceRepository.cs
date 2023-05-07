using Rey.EQueue.Core.Entities;
using Rey.EQueue.Shared.Interfaces;

namespace Rey.EQueue.Application.Repositories
{
    public interface ISubjectInstanceRepository: IRepository<SubjectInstance, int>
    {
        Task<IEnumerable<SubjectInstance>> GetBySubjectIdAsync(int sibjectId, CancellationToken cancellationToken);

        Task<IEnumerable<SubjectInstance>> GetDetailedAsync(CancellationToken cancellationToken);

        Task<SubjectInstance> GetDetailedByIdAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Timetable>> GetActiveTimetablesBySiIdAsync(int siid, CancellationToken cancellationToken);
    }
}
