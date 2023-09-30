using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Context;
using Rey.EQueue.Application.Services;
using Rey.EQueue.EF;
using Rey.EQueue.Web.Services;
using System.Security.Claims;

namespace Rey.EQueue.Web.Middlewares
{
    public class UserAccessorMiddleware
    {
        private readonly RequestDelegate _next;

        public UserAccessorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ApplicationDbContext dbContext, 
            IUserAccessor userAccessor)
        {
            var identity = context.User.Identity is not null && context.User.Identity.IsAuthenticated
                ? (ClaimsIdentity)context.User.Identity
                : null;

            if (identity is null)
            {
                userAccessor.SetUser(null);
            }
            else
            {
                var id = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = dbContext.Users
                    .Where(u => u.ApplicationUserId == id)
                    .Include(u => u.Roles)
                    .ThenInclude(ur => ur.Role)
                    .Single();

                userAccessor.SetUser(user);
            }

            await _next.Invoke(context);
        }
    }
}
