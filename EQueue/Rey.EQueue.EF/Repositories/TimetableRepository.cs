﻿using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.EF.Repositories
{
    public class TimetableRepository : Repository<Timetable>, ITimetableRepository
    {
        public TimetableRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
