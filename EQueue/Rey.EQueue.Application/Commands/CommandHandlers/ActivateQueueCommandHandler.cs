using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    public class ActivateQueueCommandHandler : IRequestHandler<ActivateQueueCommand>
    {
        private readonly IQueueRepository _queueRepository;

        public ActivateQueueCommandHandler(IQueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public async Task<Unit> Handle(ActivateQueueCommand request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.FindByIdAsync(request.QueueId, cancellationToken);
            queue.IsActive = true;

            _queueRepository.Update(queue);
            await _queueRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
