using Rey.EQueue.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Core.Entities
{
    public class SubjectInstance : Entity
    {
        public int SubjectId { get; set; }

        public Subject? Subject { get; set; }

        public int TimetableId { get; set; }

        public Timetable? Timetable { get; set; }

        public string? Description { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<SubjectInstanceTeacher>? SubjectInstanceTeachers { get; set; }

        public ICollection<ScheduledClass>? ScheduledClasses { get; set; }
    }
}
