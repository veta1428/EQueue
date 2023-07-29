using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Repositories;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetSubjectInstanceQueryHandler : IRequestHandler<GetSubjectInstanceQuery, SubjectInstanceDetailedModel>
    {
        private readonly ISubjectInstanceRepository _subjectInstanceRepository;

        public GetSubjectInstanceQueryHandler(ISubjectInstanceRepository subjectInstanceRepository) 
        { 
            _subjectInstanceRepository = subjectInstanceRepository;
        }

        public async Task<SubjectInstanceDetailedModel> Handle(GetSubjectInstanceQuery request, CancellationToken cancellationToken)
        {
            var si = await _subjectInstanceRepository.GetDetailedByIdAsync(request.SubjectInstanceId, cancellationToken);

            var tt = si.Timetables.Where(t => t.IsActualOn(DateTime.UtcNow)).SingleOrDefault();

            var classes = tt?.Classes?.Select(c => new ClassModel() { DayOfWeek = c.DayOfWeek.ToString(), Duration = c.Duration, StartTime = c.StartTime });

            return new SubjectInstanceDetailedModel()
            {
                Id = si.Id,
                InstanceDescription = si.Description,
                InstanceName = si.Name,
                SubjectName = si.Subject?.Name,
                Timetable = tt is null ? null : new TimetableModel(tt.AppliedPeriodStart, tt.AppliedPeriodEnd, classes),
                Teachers = si.SubjectInstanceTeachers
                    .Select(sit => sit.Teacher)
                    .Select(t => new TeacherModel(t.Id, t.FirstName, t.LastName, t.MiddleName, t.Description, t.Note))
            };
        }
    }
}
