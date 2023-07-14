using Rey.EQueue.Core.Enums;
using Rey.EQueue.Shared;

namespace Rey.EQueue.Core.Entities
{
    public class ChangeRequest: Entity
    {
        public int? RecordFromId { get; set; }

        public Record? RecordFrom { get; set; }

        public int? RecordToId { get; set; }

        public Record? RecordTo { get; set; }

        public DateTime Created { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        // copy from scheduled class (perf and b-logic)
        public DateTime ScheduledClassStartTime { get; set; }

        // copy from subject instance (perf and b-logic)
        public string SubjectInstanceName { get; set; } = null!;
        
        public int QueueId { get; set; }

        public int UserFromId { get; set; }

        public User? UserFrom { get; set; }

        public int UserToId { get; set; }

        public User? UserTo { get; set; }
    }
}
