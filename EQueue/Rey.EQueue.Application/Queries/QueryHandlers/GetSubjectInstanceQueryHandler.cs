using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetSubjectInstanceQueryHandler : IRequestHandler<GetSubjectInstanceQuery, SubjectInstanceDetailedModel>
    {
        private readonly ISubjectInstanceRepository _subjectInstanceRepository;
        private readonly IRoleManager _roleManager;

        public GetSubjectInstanceQueryHandler(
            ISubjectInstanceRepository subjectInstanceRepository,
            IRoleManager roleManager) 
        { 
            _subjectInstanceRepository = subjectInstanceRepository;
            _roleManager = roleManager;
        }

        public async Task<SubjectInstanceDetailedModel> Handle(GetSubjectInstanceQuery request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsUserInGroup())
                throw new InvalidOperationException("No access");

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
