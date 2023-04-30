using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class DeactivateQueueCommandHandler : IRequestHandler<DeactivateQueueCommand>
    {
        private readonly IQueueRepository _queueRepository;

        public DeactivateQueueCommandHandler(IQueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public async Task<Unit> Handle(DeactivateQueueCommand request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.FindByIdAsync(request.QueueId, cancellationToken);
            queue.IsActive = false;

            _queueRepository.Update(queue);
            await _queueRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
