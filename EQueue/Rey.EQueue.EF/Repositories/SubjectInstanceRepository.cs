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
    }
}
