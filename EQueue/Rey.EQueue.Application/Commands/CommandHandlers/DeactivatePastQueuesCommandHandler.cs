using MediatR;
using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    public class DeactivatePastQueuesCommandHandler : IRequestHandler<DeactivatePastQueuesCommand>
    {
        private readonly IScheduledClassRepository _scheduledClassRepository;
        private readonly IQueueRepository _queueRepository;

        public DeactivatePastQueuesCommandHandler(
            IScheduledClassRepository scheduledClassRepository,
            IQueueRepository queueRepository)
        {
            _scheduledClassRepository = scheduledClassRepository;
            _queueRepository = queueRepository;
        }

        public async Task<Unit> Handle(DeactivatePastQueuesCommand request, CancellationToken cancellationToken)
        {
            var currentDate = DateTime.UtcNow;
            var queuesToArchive = await _scheduledClassRepository
                .GetQuery()
                .Where(s => s.StartTime < currentDate.AddDays(-2))
                .SelectMany(s => s.Queues)
                .Where(q => q.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            foreach (var queue in queuesToArchive)
            {
                queue.IsActive = false;
            }

            _queueRepository.UpdateRange(queuesToArchive);
            await _queueRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
