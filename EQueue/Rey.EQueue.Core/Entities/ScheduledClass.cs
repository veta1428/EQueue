using Rey.EQueue.Shared;

namespace Rey.EQueue.Core.Entities
{
    public class ScheduledClass : Entity
    {
        public ScheduledClass() { }
        public ScheduledClass(DateTime startTime, int duration, int subjectInstanceId, string? description)
        {
            StartTime = startTime;
            Duration = duration;
            SubjectInstanceId = subjectInstanceId;
            Description = description;
        }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public int SubjectInstanceId { get; set; }

        public string? Description { get; set; }

        public SubjectInstance? SubjectInstance { get; set; }

        public ICollection<Queue> Queues { get; set; } = new List<Queue>();
    }
}
