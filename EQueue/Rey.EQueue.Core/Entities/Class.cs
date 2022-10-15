using Rey.EQueue.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Core.Entities
{
    public class Class : Entity
    {
        public DayOfWeek DayOfWeek { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public int TimetableId { get; set; }

        public Timetable? Timetable { get; set; }
    }
}
