using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    public class GetSubjectInstancesByTeacherQueryHandler : IRequestHandler<GetSubjectInstancesByTeacherQuery, GetSubjectInstancesQueryResult>
    {
        private readonly ISubjectInstanceRepository _subjectInstanceRepository;
        private readonly ISubjectInstanceTeacherRepository _subjectInstanceTeacherRepository;

        public GetSubjectInstancesByTeacherQueryHandler(
            ISubjectInstanceRepository subjectInstanceRepository,
            ISubjectInstanceTeacherRepository subjectInstanceTeacherRepository)
        {
            _subjectInstanceRepository = subjectInstanceRepository;
            _subjectInstanceTeacherRepository = subjectInstanceTeacherRepository;
        }

        public async Task<GetSubjectInstancesQueryResult> Handle(GetSubjectInstancesByTeacherQuery request, CancellationToken cancellationToken)
        {
            var subjectInstancesTeachers = await _subjectInstanceTeacherRepository.GetByTeacherAsync(request.TeacherId, cancellationToken);
            var sitModels = subjectInstancesTeachers.Select(sit => new SubjectInstanceModel() 
            { 
                Id = sit.SubjectInstance.Id, 
                InstanceDescription = sit.SubjectInstance.Description, 
                InstanceName = sit.SubjectInstance.Name, 
                Timetable = GetTimetableAsString(sit.SubjectInstance.Timetables) 
            });

            return new GetSubjectInstancesQueryResult(sitModels);
        }

        private List<string> GetTimetableAsString(ICollection<Timetable>? timetables)
        {
            if(timetables is null)
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
