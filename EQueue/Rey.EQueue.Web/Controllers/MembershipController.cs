using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MembershipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("current")]
        public async Task<UserModel?> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetCurrentUserQuery(), cancellationToken);
        }
    }
}
