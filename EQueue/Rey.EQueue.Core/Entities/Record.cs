using Rey.EQueue.Shared;

namespace Rey.EQueue.Core.Entities
{
    public class Record : Entity
    {
        public Queue? Queue { get; set; }

        public int QueueId { get; set; }

        public int? NextRecordId { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public IEnumerable<ChangeRequest> ChangeFrom { get; set; } = new List<ChangeRequest>();

        public IEnumerable<ChangeRequest> ChangeTo { get; set; } = new List<ChangeRequest>();
    }
}
