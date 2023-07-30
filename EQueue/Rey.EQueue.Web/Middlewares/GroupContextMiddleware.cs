using Rey.EQueue.Application.Context;

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
