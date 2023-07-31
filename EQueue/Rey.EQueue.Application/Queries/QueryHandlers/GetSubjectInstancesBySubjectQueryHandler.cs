using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetSubjectInstancesBySubjectQueryHandler : IRequestHandler<GetSubjectInstancesBySubjectQuery, GetSubjectInstancesQueryResult>
    {
        private readonly ISubjectInstanceRepository _subjectInstanceRepository;
        private readonly IRoleManager _roleManager;

        public GetSubjectInstancesBySubjectQueryHandler(
            ISubjectInstanceRepository subjectInstanceRepository,
            IRoleManager roleManager)
        {
            _subjectInstanceRepository = subjectInstanceRepository;
            _roleManager = roleManager;
        }

        public async Task<GetSubjectInstancesQueryResult> Handle(GetSubjectInstancesBySubjectQuery request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsUserInGroup())
                throw new InvalidOperationException("No access");

            var sis = (await _subjectInstanceRepository.GetBySubjectIdAsync(request.SubjectId, cancellationToken)).Select(sit => new SubjectInstanceModel()
            {
                Id = sit.Id,
                InstanceDescription = sit.Description,
                InstanceName = sit.Name,
                Classes = GetActualClasses(sit.Timetables)
            });

            return new GetSubjectInstancesQueryResult(sis);
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
