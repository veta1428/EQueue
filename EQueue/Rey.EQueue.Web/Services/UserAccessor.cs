using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;
using Rey.EQueue.EF;
using System.Security.Claims;

namespace Rey.EQueue.Web.Services
{
    public class UserAccessor : IUserAccessor
    {
        private  User? _user;

        public User? CurrentUser { get => _user; set => _user = value; }

        public void SetUser(User? user)
        {
            CurrentUser = user;
        }
    }
}
