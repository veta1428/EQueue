using MediatR;
using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetQueuesQueryHandler : IRequestHandler<GetQueuesQuery, GetQueuesQueryResult>
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IUserAccessor _userAccessor;

        public GetQueuesQueryHandler(
            IQueueRepository queueRepository,
            IUserAccessor userAccessor)
        {
            _queueRepository = queueRepository;
            _userAccessor = userAccessor;
        }

        public async Task<GetQueuesQueryResult> Handle(GetQueuesQuery request, CancellationToken cancellationToken)
        {
            var user = _userAccessor.CurrentUser ?? throw new InvalidOperationException("No user in context.");
            var queues = await (_queueRepository.GetQuery()
                .Where(q => request.Mode == QueueSearchMode.Active ? (q.IsActive == true) : (q.IsActive == false))
                .Include(q => q.Records)
                .Select(q => new
                {
                    Id = q.Id,
                    SubjectInstanceName = q.ScheduledClass!.SubjectInstance!.Name,
                    StartTime = q.ScheduledClass.StartTime,
                    PeopleIn = q.Records.Count(),
                    Records = q.Records,
                    IsActive = q.IsActive,
                })
                .ToArrayAsync(cancellationToken));

            var result = new List<QueueModel>();

            foreach (var queue in queues)
            {
                result.Add(new QueueModel
                {
                    Id = queue.Id,
                    StartTime = queue.StartTime,
                    SubjectInstanceName = queue.SubjectInstanceName,
                    PeopleIn = queue.PeopleIn,
                    CurrentUserPosition = GetCurrentUserPosition(user.Id, queue.Records),
                    IsActive = queue.IsActive,
                });
            }

            return new GetQueuesQueryResult(result);
        }

        private int GetCurrentUserPosition(int userId, ICollection<Record> records)
        {
            var tail = records.SingleOrDefault(r => r.NextRecordId == null);

            if (tail is null)
            {
                return -1;
            }

            var current = tail;
            int counter = records.Count;
            while (true)
            {
                if (current.UserId == userId)
                    return counter;

                current = records.SingleOrDefault(r => r.NextRecordId == current.Id);
                if (current is null)
                {
                    return -1;
                }
                counter--;
            }
        }
    }
}
