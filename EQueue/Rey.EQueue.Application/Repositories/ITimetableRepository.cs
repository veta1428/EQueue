using Rey.EQueue.Core.Entities;
using Rey.EQueue.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Application.Repositories
{
    public interface ITimetableRepository: IRepository<Timetable, int>
    {
    }
}
