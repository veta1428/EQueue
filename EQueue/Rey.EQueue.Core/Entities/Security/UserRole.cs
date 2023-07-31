
namespace Rey.EQueue.Core.Entities.Security
{
    public class UserRole
    {
        public int UserId { get; set; }

        public Role Role { get; set; } = null!;

        public int RoleId { get; set; }

        public int GroupId { get; set; }
    }
}
