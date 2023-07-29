using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetSubjectInstancesQueryHandler : IRequestHandler<GetSubjectInstancesQuery, GetSubjectInstancesQueryResult>
    {

        private readonly ISubjectInstanceRepository _subjectInstanceRepository;

        public GetSubjectInstancesQueryHandler(ISubjectInstanceRepository subjectInstanceRepository)
        {
            _subjectInstanceRepository = subjectInstanceRepository;
        }

        public async Task<GetSubjectInstancesQueryResult> Handle(GetSubjectInstancesQuery request, CancellationToken cancellationToken)
        {
            var subjectInstances = (await _subjectInstanceRepository.GetDetailedAsync(cancellationToken)).Select(subjectInstance => new SubjectInstanceModel()
            {
                Id = subjectInstance.Id,
                InstanceDescription = subjectInstance.Description,
                InstanceName = subjectInstance.Name,
                Classes = GetActualClasses(subjectInstance.Timetables)
            });

            return new GetSubjectInstancesQueryResult(subjectInstances);
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
