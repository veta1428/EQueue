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
        [Route("queues/{mode}")]
        public async Task<GetQueuesQueryResult> GetQueues(QueueSearchMode mode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetQueuesQuery(mode), cancellationToken);
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

        [HttpPost]
        [Route("add")]
        public async Task<int> AddQueue([FromBody] AddQueueCommand queue, CancellationToken cancellationToken)
        {
            return await _mediator.Send(queue, cancellationToken);
        }

        [HttpPost]
        [Route("deactivate/{id}")]
        public async Task DeactivateQueue(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeactivateQueueCommand(id), cancellationToken);
        }

        [HttpPost]
        [Route("activate/{id}")]
        public async Task ActivateQueue(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new ActivateQueueCommand(id), cancellationToken);
        }
    }
}
