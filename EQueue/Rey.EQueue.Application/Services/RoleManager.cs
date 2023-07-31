using Rey.EQueue.Application.Context;

namespace Rey.EQueue.Application.Services
{
    public class RoleManager : IRoleManager
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IGroupContextAccessor _groupContextAccessor;

        public RoleManager(IUserAccessor userAccessor, IGroupContextAccessor groupContextAccessor)
        {
            _userAccessor = userAccessor;
            _groupContextAccessor = groupContextAccessor;
        }

        public bool IsAdmin()
        {
            return UserInRole("admin");
        }

        public bool IsUser()
        {
            return UserInRole("user");
        }

        private bool UserInRole(string role)
        {
            int groupId = _groupContextAccessor.Current?.GroupId
                ?? throw new InvalidOperationException($"No {nameof(GroupContext.GroupId)} found");

            var user = _userAccessor.CurrentUser
                ?? throw new InvalidOperationException($"No user in context found");

            if (user.Roles.Where(ur => ur.GroupId == groupId).Any(ur => ur.Role.Name.ToLower() == role.ToLower()))
                return true;

            return false;
        }
    }
}
