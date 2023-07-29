using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class GenerateQueuesCommandHandler : IRequestHandler<GenerateQueuesCommand>
    {
        private readonly ISubjectInstanceRepository _subjectInstanceRepository;
        private readonly IScheduledClassRepository _scheduledClassRepository;
        private readonly IMediator _mediator;

        public GenerateQueuesCommandHandler(
            ISubjectInstanceRepository subjectInstanceRepository,
            IScheduledClassRepository scheduledClassRepository,
            IMediator mediator)
        {
            _subjectInstanceRepository = subjectInstanceRepository;
            _scheduledClassRepository = scheduledClassRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(GenerateQueuesCommand request, CancellationToken cancellationToken)
        {
            var currentDay = DateTime.UtcNow;
            var currentDayOfWeek = currentDay.DayOfWeek;

            var subjectInstances = await _subjectInstanceRepository.GetDetailedAsync(cancellationToken);

            // find with actual timetable
            var withTimetable = subjectInstances
                .Where(si => si.Timetables.Any(t => t.IsActualOn(currentDay)))
                .Select(si => new
                {
                    SubjectInstanceId = si.Id,
                    Classes = si.Timetables
                    .Where(t => t.IsActualOn(currentDay)).Single().Classes ?? new List<Class>()
                });

            List<ScheduledClassModel> toCreate = new List<ScheduledClassModel>();

            foreach (var timetable in withTimetable)
            {
                foreach (var @class in timetable.Classes)
                {
                    toCreate.Add(new ScheduledClassModel() { 
                        SubjectInstanceId = timetable.SubjectInstanceId,
                        StartTime = CalculateClassDateTime(@class, currentDay),
                        Duration = @class.Duration,
                    });
                }
            }

            foreach (var candidate in toCreate)
            {
                if(!await Exists(candidate.SubjectInstanceId, candidate.StartTime, cancellationToken))
                {
                    await _mediator.Send(new AddQueueCommand() 
                    { 
                        SubjectInstanceId = candidate.SubjectInstanceId, 
                        Duration = candidate.Duration, 
                        StartTime = candidate.StartTime, 
                        Description = null 
                    }, cancellationToken);
                }
            }

            return Unit.Value;
        }

        private async Task<bool> Exists(int subjectInstanceId, DateTime dateTime, CancellationToken cancellationToken)  
            => (await _scheduledClassRepository
            .TryGetBySiIdAndStartTimeAsync(subjectInstanceId, dateTime, cancellationToken)) is not null;
        

        private DateTime CalculateClassDateTime(Class @class, DateTime current)
        {
            var daysToAdd = GetDaysToAdd(current.DayOfWeek, @class.DayOfWeek);
            var date = current.AddDays(daysToAdd);
            return new DateTime(
                year: date.Year, 
                month: date.Month, 
                day: date.Day, 
                hour: @class.StartTime.Hour, 
                minute: @class.StartTime.Minute, 
                second: @class.StartTime.Second);
        }
 
        private int GetDaysToAdd(DayOfWeek current, DayOfWeek byTimetable)
            => current < byTimetable
                ? byTimetable - current
                : 7 - (current - byTimetable);
    }

    internal class ScheduledClassModel
    {
        public int SubjectInstanceId { get; init; }

        public DateTime StartTime { get; init; }

        public int Duration { get; init; }
    }
}
