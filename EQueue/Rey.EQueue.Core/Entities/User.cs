using Rey.EQueue.Shared;

namespace Rey.EQueue.Core.Entities
{
    public class User : Entity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public ICollection<Record> Records { get; set; } = null!;

        public string ApplicationUserId { get; set; } = null!;
    }
}
