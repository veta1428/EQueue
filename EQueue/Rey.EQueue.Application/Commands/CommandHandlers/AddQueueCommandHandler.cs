using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddQueueCommandHandler : IRequestHandler<AddQueueCommand, int>
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IMediator _mediator;

        public AddQueueCommandHandler(
            IQueueRepository queueRepository, 
            IMediator mediator)
        {
            _queueRepository = queueRepository;
            _mediator = mediator;
        }

        public async Task<int> Handle(AddQueueCommand request, CancellationToken cancellationToken)
        {
            var addSchClassCommand = new AddScheduledClassCommand()
            {
                Duration = request.Duration,
                SubjectInstanceId = request.SubjectInstanceId,
                Description = null,
                StartTime = request.StartTime
            };

            var schClassId = await _mediator.Send(addSchClassCommand, cancellationToken);

            var queue = new Queue()
            {
                CreationDate = DateTime.UtcNow,
                Description = request.Description,
                IsActive = true,
                ScheduledClassId = schClassId
            };

            _queueRepository.Add(queue);
            await _queueRepository.SaveChangesAsync(cancellationToken);

            return queue.Id;
        }
    }
}
