using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Context;
using Rey.EQueue.Application.Services;
using Rey.EQueue.EF;
using System.Security.Claims;

namespace Rey.EQueue.Web.Middlewares
{
    public class GroupContextMiddleware
    {
        private readonly RequestDelegate _next;

        public GroupContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IGroupContextScheduler scheduler)
        {
            await scheduler.ExecuteAsync(
                async () => await _next.Invoke(context), null, context.RequestAborted);
        }
    }
}
