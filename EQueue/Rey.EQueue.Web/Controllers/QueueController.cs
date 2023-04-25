using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryResults;

namespace Rey.EQueue.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QueueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("queues")]
        public async Task<GetQueuesQueryResult> GetQueues(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetQueuesQuery(), cancellationToken);
        }

        [HttpGet]
        [Route("{queueId}")]
        public async Task<GetRecordsByQueueQueryResult> GetQueue(int queueId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetRecordsByQueueQuery(queueId), cancellationToken);
        }

        [HttpGet]
        [Route("add-user/{queueId}")]
        public async Task AddUser(int queueId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new AddUserToQueueCommand(queueId), cancellationToken);
        }

        [HttpGet]
        [Route("remove-user/{queueId}")]
        public async Task RemoveUser(int queueId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RemoveUserFromQueueCommand(queueId), cancellationToken);
        }
    }
}
