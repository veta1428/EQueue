using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddScheduledClassCommandHandler : IRequestHandler<AddScheduledClassCommand, int>
    {
        private readonly IScheduledClassRepository _scheduledClass;

        public AddScheduledClassCommandHandler(IScheduledClassRepository scheduledClass)
        {
            _scheduledClass = scheduledClass;
        }

        public async Task<int> Handle(AddScheduledClassCommand request, CancellationToken cancellationToken)
        {
            var scheduledClass = new ScheduledClass(request.StartTime, request.Duration, request.SubjectInstanceId, request.Description);

            _scheduledClass.Add(scheduledClass);

            await _scheduledClass.SaveChangesAsync(cancellationToken);

            return scheduledClass.Id;
        }
    }
}
