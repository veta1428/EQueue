using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Options;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    public class DeactivateQueuesCommandHandler 
        : IRequestHandler<DeactivateQueuesCommand>
    {
        private readonly IScheduledClassRepository _scheduledClassRepository;
        private readonly IQueueRepository _queueRepository;
        private readonly DeactivateQueueOptions _options;
        private readonly IRoleManager _roleManager;

        public DeactivateQueuesCommandHandler(
            IScheduledClassRepository scheduledClassRepository,
            IQueueRepository queueRepository,
            IOptions<DeactivateQueueOptions> options,
            IRoleManager roleManager)
        {
            _scheduledClassRepository = scheduledClassRepository;
            _queueRepository = queueRepository;
            _options = options.Value;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(
            DeactivateQueuesCommand request, 
            CancellationToken cancellationToken)
        {
            if (!_roleManager.IsAdminInGroup())
                throw new InvalidOperationException("No access");

            var currentDate = DateTime.UtcNow;
            var queuesToArchive = await _scheduledClassRepository
                .GetQuery()
                .Where(s => s.StartTime < currentDate.AddDays(-_options.KeepQueueActive))
                .SelectMany(s => s.Queues)
                .Where(q => q.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            foreach (var queue in queuesToArchive)
                queue.IsActive = false;

            _queueRepository.UpdateRange(queuesToArchive);
            await _queueRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
