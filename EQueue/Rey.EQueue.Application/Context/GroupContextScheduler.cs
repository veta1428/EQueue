using Microsoft.AspNetCore.Http;

namespace Rey.EQueue.Application.Context
{
    public class GroupContextScheduler : IGroupContextScheduler
    {
        private readonly IGroupContextAccessor _groupAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GroupContextScheduler(IGroupContextAccessor groupAccessor, IHttpContextAccessor httpContextAccessor) 
        { 
            _groupAccessor = groupAccessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ExecuteAsync(Func<Task> func, int? groupId, CancellationToken cancellationToken)
        {
            groupId ??= TryGetGroupId();
            GroupContext? gctx = null;

            if (groupId.HasValue)
            {
                gctx = new GroupContext(groupId.Value);
            }

            using (_groupAccessor.UseGroupContext(gctx))
            {
                await func();
            }
        }

        private int? TryGetGroupId()
        {
            var headers = _httpContextAccessor.HttpContext?.Request.Headers;

            //foreach (var item in headers)
            //{
            //    Console.WriteLine(item.Key + ": " + item.Value);
            //}

            int? res = headers is not null && headers.TryGetValue("Group-Id", out var values) && values.Any() && int.TryParse(values[0], out var groupId)
                ? groupId
                : null;
            if (res is null)
            {
                Console.WriteLine(_httpContextAccessor.HttpContext.Request.Path);
            }
            else
                Console.WriteLine(_httpContextAccessor.HttpContext.Request.Path + "***********" + res);
            return res;
        }
    }
}
