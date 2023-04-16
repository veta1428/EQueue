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
    public class SubjectInstanceRepository : Repository<SubjectInstance>, ISubjectInstanceRepository
    {
        public SubjectInstanceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SubjectInstance>> GetBySubjectIdAsync(int subjectId, CancellationToken cancellationToken)
        {
            return await GetQuery()
                .Where(si => si.SubjectId == subjectId)
                .ToListAsync(cancellationToken);
        }
    }
}
