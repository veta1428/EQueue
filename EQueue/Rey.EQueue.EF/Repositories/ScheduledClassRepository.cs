using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Repositories
{
    public class ScheduledClassRepository : Repository<ScheduledClass>, IScheduledClassRepository
    {
        public ScheduledClassRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ScheduledClass?> TryGetBySiIdAndStartTimeAsync(int siid, DateTime time, CancellationToken cancellationToken)
        {
            return await GetQuery().Where(sc => sc.SubjectInstanceId == siid && sc.StartTime == time).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
