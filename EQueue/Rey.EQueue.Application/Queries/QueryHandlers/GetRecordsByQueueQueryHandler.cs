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
        private readonly IRoleManager _roleManager;

        public GetRecordsByQueueQueryHandler(
            IQueueRepository queueRepository,
            IUserAccessor userAccessor,
            IRecordRepository recordRepository,
            IRoleManager roleManager)
        {
            _queueRepository = queueRepository;
            _userAccessor = userAccessor;
            _recordRepository = recordRepository;
            _roleManager = roleManager;
        }

        public async Task<GetRecordsByQueueQueryResult> Handle(GetRecordsByQueueQuery request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsUserInGroup())
                throw new InvalidOperationException("No access");

            var user = _userAccessor.CurrentUser ?? throw new InvalidOperationException("No user in context.");
            var queue = await (_queueRepository.GetQuery()
                .Where(q => q.Id == request.QueueId)
                .Include(q => q.Records)
                .Select(q => new
                {
                    Id = q.Id,
                    SubjectInstanceName = q.ScheduledClass!.SubjectInstance!.Name,
                    StartTime = q.ScheduledClass.StartTime,
                    IsActive = q.IsActive
                })
                .SingleAsync(cancellationToken));

            var records = await _recordRepository.GetRecordsByQueueIdAsync(request.QueueId, cancellationToken);

            records.Sort(CompareRecords);

            int startPosition = 1;

            return new GetRecordsByQueueQueryResult(
                queue.Id,
                queue.SubjectInstanceName,
                queue.StartTime,
                queue.IsActive,
                records.Select(r => new RecordModel() 
                { 
                    Created = r.CreationDate,
                    StudentFirstName = r.User!.FirstName,
                    StudentLastName = r.User!.LastName,
                    RecordId = r.Id,
                    Position = startPosition++,
                    IsCurrentUser = r.UserId == user.Id,
                    CanSendRequest = !r.ChangeTo.Where(chr => chr.UserFromId == user.Id && chr.Status == Core.Enums.RequestStatus.Pending).Any(),
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
