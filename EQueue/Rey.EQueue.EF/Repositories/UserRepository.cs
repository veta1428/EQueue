using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.EF.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
