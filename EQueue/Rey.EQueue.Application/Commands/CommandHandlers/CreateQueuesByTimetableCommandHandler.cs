using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;
using System;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class CreateQueuesByTimetableCommandHandler : IRequestHandler<CreateQueuesByTimetableCommand>
    {
        private readonly ISubjectInstanceRepository _subjectInstanceRepository;
        private readonly IScheduledClassRepository _scheduledClassRepository;
        private readonly IMediator _mediator;

        public CreateQueuesByTimetableCommandHandler(
            ISubjectInstanceRepository subjectInstanceRepository,
            IScheduledClassRepository scheduledClassRepository,
            IMediator mediator)
        {
            _subjectInstanceRepository = subjectInstanceRepository;
            _scheduledClassRepository = scheduledClassRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateQueuesByTimetableCommand request, CancellationToken cancellationToken)
        {
            var currentDay = DateTime.UtcNow;
            var currentDayOfWeek = currentDay.DayOfWeek;

            var sis = await _subjectInstanceRepository.GetDetailedAsync(cancellationToken);

            // finding with timetable
            var hasTT = sis
                .Where(si => si.Timetables.Any(tt => tt.IsActive && tt.AppliedPeriodStart < currentDay && tt.AppliedPeriodEnd > currentDay))
                .Select(si => new
                {
                    SubjectInstanceId = si.Id,
                    Classes = si.Timetables.Where(tt => tt.IsActive && tt.AppliedPeriodStart < currentDay && tt.AppliedPeriodEnd > currentDay).Single().Classes ?? new List<Class>()
                });

            // info for queue Creating
            List<(int, Class)> info = new List<(int, Class)> ();

            foreach (var qData in hasTT)
            {
                foreach (var @class in qData.Classes)
                {
                    info.Add((qData.SubjectInstanceId, @class));
                }
            }

            var create = info.Select(i => (i.Item1, GetDateTime(i.Item2, currentDay), i.Item2.Duration));

            foreach (var row in create)
            {
                if(!await IsExists(row.Item1, row.Item2, cancellationToken))
                {
                    await _mediator.Send(new AddQueueCommand() { SubjectInstanceId = row.Item1, Duration = row.Duration, StartTime = row.Item2, Description = null }, cancellationToken);
                }
            }

            return Unit.Value;
        }

        private async Task<bool> IsExists(int subjectInstanceId, DateTime dateTime, CancellationToken cancellationToken)
        {
            var sc = await _scheduledClassRepository.TryGetBySiIdAndStartTimeAsync(subjectInstanceId, dateTime, cancellationToken);
            return sc is null ? false : true;
        }

        private DateTime GetDateTime(Class @class, DateTime current)
        {
            var daysToAdd = GetDaysToAdd(current.DayOfWeek, @class.DayOfWeek);
            var date = current.AddDays(daysToAdd);
            return new DateTime(year: date.Year, month: date.Month, day: date.Day, hour: @class.StartTime.Hour, minute: @class.StartTime.Minute, second: @class.StartTime.Second);
        }

        private int GetDaysToAdd(DayOfWeek current, DayOfWeek tt)
        {
            if(current < tt)
                return tt - current;

            return 7 - (current - tt);
        }
    }
}
