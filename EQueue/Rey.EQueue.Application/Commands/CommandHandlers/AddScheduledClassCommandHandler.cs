using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class AddScheduledClassCommandHandler : IRequestHandler<AddScheduledClassCommand, int>
    {
        private readonly IScheduledClassRepository _scheduledClass;
        private readonly IRoleManager _roleManager;

        public AddScheduledClassCommandHandler(IScheduledClassRepository scheduledClass, IRoleManager roleManager)
        {
            _scheduledClass = scheduledClass;
            _roleManager = roleManager;
        }

        public async Task<int> Handle(AddScheduledClassCommand request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsAdminInGroup())
                throw new InvalidOperationException("No access");

            var scheduledClass = new ScheduledClass(request.StartTime, request.Duration, request.SubjectInstanceId, request.Description);

            _scheduledClass.Add(scheduledClass);

            await _scheduledClass.SaveChangesAsync(cancellationToken);

            return scheduledClass.Id;
        }
    }
}
