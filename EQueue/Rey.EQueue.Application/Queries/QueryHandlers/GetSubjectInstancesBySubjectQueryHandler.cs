using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetSubjectInstancesBySubjectQueryHandler : IRequestHandler<GetSubjectInstancesBySubjectQuery, GetSubjectInstancesQueryResult>
    {
        private readonly ISubjectInstanceRepository _subjectInstanceRepository;

        public GetSubjectInstancesBySubjectQueryHandler(ISubjectInstanceRepository subjectInstanceRepository)
        {
            _subjectInstanceRepository = subjectInstanceRepository;
        }

        public async Task<GetSubjectInstancesQueryResult> Handle(GetSubjectInstancesBySubjectQuery request, CancellationToken cancellationToken)
        {
            var sis = (await _subjectInstanceRepository.GetBySubjectIdAsync(request.SubjectId, cancellationToken)).Select(sit => new SubjectInstanceModel()
            {
                Id = sit.Id,
                InstanceDescription = sit.Description,
                InstanceName = sit.Name,
                Timetable = GetTimetableAsString(sit.Timetables)
            });

            return new GetSubjectInstancesQueryResult(sis);
        }

        private List<string> GetTimetableAsString(ICollection<Timetable>? timetables)
        {
            if (timetables is null)
            {
                return new List<string>();
            }
            var activeTimetable = timetables.Where(t => t.AppliedPeriodStart < DateTime.UtcNow && t.AppliedPeriodEnd > DateTime.UtcNow).FirstOrDefault();

            if (activeTimetable is null)
            {
                return new List<string>();
            }

            return activeTimetable.Classes?.Select(c => c.DayOfWeek.ToString() + " at " + c.StartTime.ToString("HH:mm (") + c.Duration.ToString() + " min)").ToList() ?? new List<string>();
        }
    }
}
