using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.EF.Repositories
{
    public class SubjectInstanceTeacherRepository : Repository<SubjectInstanceTeacher>, ISubjectInstanceTeacherRepository
    {
        public SubjectInstanceTeacherRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SubjectInstanceTeacher>> GetByTeacherAsync(int teacherId, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(sit => sit.TeacherId == teacherId)
                .Include(sit => sit.SubjectInstance)
                .ThenInclude(si => si.Timetables)
                .ThenInclude(t => t.Classes)
                .ToListAsync(cancellationToken);
        }
    }
}
