using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Repositories
{
    public class TimetableRepository : Repository<Timetable>, ITimetableRepository
    {
        public TimetableRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
