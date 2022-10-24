using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.EF.Repositories
{
    internal class SubjectInstanceTeacherRepository : Repository<SubjectInstanceTeacher>, ISubjectInstanceTeacherRepository
    {
        public SubjectInstanceTeacherRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
