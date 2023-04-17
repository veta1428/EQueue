using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Services
{
    public interface IUserAccessor
    {
        public User? CurrentUser { get; protected set; }

        void SetUser(User? user);
    }
}
