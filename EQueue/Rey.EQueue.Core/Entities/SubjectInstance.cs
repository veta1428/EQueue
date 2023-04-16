using Rey.EQueue.Shared;

namespace Rey.EQueue.Core.Entities
{
    public class SubjectInstance : Entity
    {
        public int SubjectId { get; set; }

        public Subject? Subject { get; set; }

        public ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();

        public string? Description { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<SubjectInstanceTeacher> SubjectInstanceTeachers { get; set; } = new List<SubjectInstanceTeacher>();

        public ICollection<ScheduledClass>? ScheduledClasses { get; set; }
    }
}
