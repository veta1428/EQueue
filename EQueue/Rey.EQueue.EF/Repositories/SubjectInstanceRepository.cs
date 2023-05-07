using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Repositories
{
    public class SubjectInstanceRepository : Repository<SubjectInstance>, ISubjectInstanceRepository
    {
        public SubjectInstanceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SubjectInstance>> GetBySubjectIdAsync(int subjectId, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(si => si.SubjectId == subjectId)
                .Include(si => si.Timetables)
                .ThenInclude(tt => tt.Classes)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<SubjectInstance>> GetDetailedAsync(CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Include(si => si.Timetables)
                .ThenInclude(tt => tt.Classes)
                .ToListAsync(cancellationToken);
        }

        public async Task<SubjectInstance> GetDetailedByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(si => si.Id == id)
                .Include(si => si.Subject)
                .Include(si => si.SubjectInstanceTeachers)
                    .ThenInclude(sit => sit.Teacher)
                .Include(si => si.Timetables)
                    .ThenInclude(tt => tt.Classes)
                .SingleAsync(cancellationToken);
        }

        public async Task<IEnumerable<Timetable>> GetActiveTimetablesBySiIdAsync(int siid, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(si => si.Id == siid)
                .SelectMany(si => si.Timetables)
                .Where(t => t.AppliedPeriodEnd > DateTime.UtcNow && t.AppliedPeriodStart < DateTime.UtcNow && t.IsActive)
                .ToListAsync(cancellationToken);
        }
    }
}
