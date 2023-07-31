using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    public class ActivateQueueCommandHandler : IRequestHandler<ActivateQueueCommand>
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IRoleManager _roleManager;

        public ActivateQueueCommandHandler(IQueueRepository queueRepository, IRoleManager roleManager)
        {
            _queueRepository = queueRepository;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(ActivateQueueCommand request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsAdminInGroup())
                throw new InvalidOperationException("No access");

            var queue = await _queueRepository.FindByIdAsync(request.QueueId, cancellationToken);
            queue.IsActive = true;

            _queueRepository.Update(queue);
            await _queueRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
