using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetGroups")]
        public async Task<IEnumerable<GroupModel>> GetGroups(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetGroupsQuery(), cancellationToken);
        }
    }
}
