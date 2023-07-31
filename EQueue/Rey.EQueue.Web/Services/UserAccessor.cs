using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;
using Rey.EQueue.EF;
using System.Security.Claims;

namespace Rey.EQueue.Web.Services
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public UserAccessor(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext is null)
            {
                SetUser(null);
                return;
            }

            var identity = httpContext.User.Identity is not null && httpContext.User.Identity.IsAuthenticated
                ? (ClaimsIdentity)httpContext.User.Identity
                : null;

            if (identity is null)
            {
                SetUser(null);
            }
            else
            {
                var id = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = _context.Users
                    .Where(u => u.ApplicationUserId == id)
                    .Include(u => u.Roles)
                    .ThenInclude(ur => ur.Role)
                    .Single();

                SetUser(user);
            }
        }

        private  User? _user;

        public User? CurrentUser { get => _user; set => _user = value; }

        public void SetUser(User? user)
        {
            CurrentUser = user;
        }
    }
}
