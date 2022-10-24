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
    }
}
