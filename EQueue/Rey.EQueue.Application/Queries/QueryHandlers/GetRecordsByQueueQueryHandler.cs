using MediatR;
using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetRecordsByQueueQueryHandler : IRequestHandler<GetRecordsByQueueQuery, GetRecordsByQueueQueryResult>
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IUserAccessor _userAccessor;
        private readonly IRecordRepository _recordRepository;

        public GetRecordsByQueueQueryHandler(
            IQueueRepository queueRepository,
            IUserAccessor userAccessor,
            IRecordRepository recordRepository)
        {
            _queueRepository = queueRepository;
            _userAccessor = userAccessor;
            _recordRepository = recordRepository;
        }

        public async Task<GetRecordsByQueueQueryResult> Handle(GetRecordsByQueueQuery request, CancellationToken cancellationToken)
        {
            var user = _userAccessor.CurrentUser ?? throw new InvalidOperationException("No user in context.");
            var queue = await (_queueRepository.GetQuery()
                .Where(q => q.IsActive && q.Id == request.QueueId)
                .Select(q => new
                {
                    Id = q.Id,
                    SubjectInstanceName = q.ScheduledClass!.SubjectInstance!.Name,
                    StartTime = q.ScheduledClass.StartTime
                })
                .SingleAsync(cancellationToken));

            var records = await _recordRepository.GetRecordsByQueueIdAsync(request.QueueId, cancellationToken);

            records.Sort(CompareRecords);

            int startPosition = 1;

            return new GetRecordsByQueueQueryResult(
                queue.Id,
                queue.SubjectInstanceName,
                queue.StartTime,
                records.Select(r => new RecordModel() 
                { 
                    Created = r.CreationDate,
                    StudentFirstName = r.User!.FirstName,
                    StudentLastName = r.User!.LastName,
                    RecordId = r.Id,
                    Position = startPosition++,
                    IsCurrentUser = r.UserId == user.Id,
                }));
        }

        private int CompareRecords(Record first, Record second)
        {
            var f = first.NextRecordId;

            if (!f.HasValue)
                return 1;

            return first.NextRecordId!.Value.CompareTo(second.Id);
        }
    }
}
