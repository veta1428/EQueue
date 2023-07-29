using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Context;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddQueueCommandHandler : IRequestHandler<AddQueueCommand, int>
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IMediator _mediator;
        private readonly IGroupContextAccessor _groupContextAccessor;

        public AddQueueCommandHandler(
            IQueueRepository queueRepository, 
            IMediator mediator,
            IGroupContextAccessor groupContextAccessor)
        {
            _queueRepository = queueRepository;
            _mediator = mediator;
            _groupContextAccessor = groupContextAccessor;
        }

        public async Task<int> Handle(AddQueueCommand request, CancellationToken cancellationToken)
        {
            int groupId = _groupContextAccessor.Current?.GroupId ?? throw new InvalidOperationException($"{nameof(Queue.GroupId)} is absent");

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
                ScheduledClassId = schClassId,
                GroupId = groupId,
            };

            _queueRepository.Add(queue);
            await _queueRepository.SaveChangesAsync(cancellationToken);

            return queue.Id;
        }
    }
}
