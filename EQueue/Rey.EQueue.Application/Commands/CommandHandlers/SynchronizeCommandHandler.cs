using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Context;
using Rey.EQueue.Application.Repositories;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class SynchronizeCommandHandler : IRequestHandler<SynchronizeCommand>
    {
        private readonly IMediator _mediator;
        private readonly IGroupContextScheduler _scheduler;
        private readonly IGroupRepository _groupRepository;

        public SynchronizeCommandHandler(IMediator mediator, IGroupContextScheduler scheduler, IGroupRepository groupRepository)
        {
            _mediator = mediator;
            _scheduler = scheduler;
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(SynchronizeCommand request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetAsync(cancellationToken);

            foreach (var group in groups)
            {
                await _scheduler.ExecuteAsync(async () =>
                {
                    await _mediator.Send(new DeactivateQueuesCommand(), cancellationToken);
                    await _mediator.Send(new GenerateQueuesCommand(), cancellationToken);
                }, group.Id, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
