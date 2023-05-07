using MediatR;
using Rey.EQueue.Application.Commands.Commands;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class SynchronizeCommandHandler : IRequestHandler<SynchronizeCommand>
    {
        private readonly IMediator _mediator;

        public SynchronizeCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SynchronizeCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeactivatePastQueuesCommand(), cancellationToken);
            await _mediator.Send(new CreateQueuesByTimetableCommand(), cancellationToken);
            return Unit.Value;
        }
    }
}
