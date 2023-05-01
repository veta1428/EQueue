using Rey.EQueue.Core.Enums;
using Rey.EQueue.Shared;

namespace Rey.EQueue.Core.Entities
{
    public class ChangeRequest: Entity
    {
        public int? RecordFromId { get; set; }

        public int? RecordToId { get; set; }

        public DateTime Created { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        public Record? RecordFrom { get; set; }

        public Record? RecordTo { get; set; }

        public DateTime StartTime { get; set; }

        public string SubjectInstanceName { get; set; } = null!;
        

        public int QueueId { get; set; }

        public int UserFromId { get; set; }

        public string UserFromFirstName { get; set; } = null!;

        public string UserFromLastName { get; set; } = null!;

        public int UserToId { get; set; }

        public string UserToFirstName { get; set; } = null!;

        public string UserToLastName { get; set; } = null!;
    }
}
