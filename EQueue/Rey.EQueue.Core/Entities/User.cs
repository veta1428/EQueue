using Rey.EQueue.Core.Entities.Security;
using Rey.EQueue.Shared;

namespace Rey.EQueue.Core.Entities
{
    public class User : Entity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

        public ICollection<Record> Records { get; set; } = null!;

        public string ApplicationUserId { get; set; } = null!;
    }
}
