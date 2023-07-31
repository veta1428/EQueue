using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Context;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class SynchronizeCommandHandler : IRequestHandler<SynchronizeCommand>
    {
        private readonly IMediator _mediator;
        private readonly IGroupContextScheduler _scheduler;
        private readonly IGroupRepository _groupRepository;
        private readonly IRoleManager _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IUserAccessor _userAccessor;

        public SynchronizeCommandHandler(
            IMediator mediator, 
            IGroupContextScheduler scheduler, 
            IGroupRepository groupRepository,
            IRoleManager roleManager,
            IUserRepository userRepository,
            IUserAccessor userAccessor)
        {
            _mediator = mediator;
            _scheduler = scheduler;
            _groupRepository = groupRepository;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(SynchronizeCommand request, CancellationToken cancellationToken)
        {
            var systemUser = await _userRepository.GetSystemUserAsync(cancellationToken);
            _userAccessor.SetUser(systemUser);

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
