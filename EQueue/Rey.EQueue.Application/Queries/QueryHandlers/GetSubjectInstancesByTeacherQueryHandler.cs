using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    public class GetSubjectInstancesByTeacherQueryHandler : IRequestHandler<GetSubjectInstancesByTeacherQuery, GetSubjectInstancesQueryResult>
    {
        private readonly ISubjectInstanceRepository _subjectInstanceRepository;
        private readonly ISubjectInstanceTeacherRepository _subjectInstanceTeacherRepository;
        private readonly IRoleManager _roleManager;

        public GetSubjectInstancesByTeacherQueryHandler(
            ISubjectInstanceRepository subjectInstanceRepository,
            ISubjectInstanceTeacherRepository subjectInstanceTeacherRepository,
            IRoleManager roleManager)
        {
            _subjectInstanceRepository = subjectInstanceRepository;
            _subjectInstanceTeacherRepository = subjectInstanceTeacherRepository;
            _roleManager = roleManager;
        }

        public async Task<GetSubjectInstancesQueryResult> Handle(GetSubjectInstancesByTeacherQuery request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsUserInGroup())
                throw new InvalidOperationException("No access");

            var subjectInstancesTeachers = await _subjectInstanceTeacherRepository.GetByTeacherAsync(request.TeacherId, cancellationToken);
            var sitModels = subjectInstancesTeachers.Select(sit => new SubjectInstanceModel() 
            { 
                Id = sit.SubjectInstance.Id, 
                InstanceDescription = sit.SubjectInstance.Description, 
                InstanceName = sit.SubjectInstance.Name, 
                Classes = GetActualClasses(sit.SubjectInstance.Timetables) 
            });

            return new GetSubjectInstancesQueryResult(sitModels);
        }

        private IEnumerable<ClassModel> GetActualClasses(ICollection<Timetable>? timetables)
        {
            if (timetables is null)
                return new List<ClassModel>();

            var activeTimetable = timetables.Where(t => t.AppliedPeriodStart < DateTime.UtcNow && t.AppliedPeriodEnd > DateTime.UtcNow && t.IsActive).FirstOrDefault();

            if (activeTimetable is null)
                return new List<ClassModel>();

            return activeTimetable.Classes?.Select(@class => new ClassModel() { DayOfWeek = @class.DayOfWeek.ToString(), Duration = @class.Duration, StartTime = @class.StartTime }) ?? new List<ClassModel>();
        }
    }
}
