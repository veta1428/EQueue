using MediatR;
using Rey.EQueue.Application.Queries.Queries;
using Rey.EQueue.Application.Queries.QueryResults;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;
using Rey.EQueue.Core.Enums;

namespace Rey.EQueue.Application.Queries.QueryHandlers
{
    internal class GetChangeRequestsQueryHandler : IRequestHandler<GetChangeRequestsQuery, GetChangeRequestsQueryResult>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IChangeRequestRepository _changeRequestRepository;
        private readonly IQueueRepository _queueRepository;

        private Dictionary<int, IEnumerable<Record>> _queueInfo;

        public GetChangeRequestsQueryHandler(
            IUserAccessor userAccessor,
            IUserRepository userRepository,
            IChangeRequestRepository changeRequestRepository,
            IQueueRepository queueRepository)
        {
            _userAccessor = userAccessor;
            _userRepository = userRepository;
            _changeRequestRepository = changeRequestRepository;
            _queueRepository = queueRepository;
        }

        public async Task<GetChangeRequestsQueryResult> Handle(GetChangeRequestsQuery request, CancellationToken cancellationToken)
        {
            
            var currentUser = _userAccessor.CurrentUser ?? throw new InvalidOperationException();


            IEnumerable<ChangeRequest>? changeRequests = null;

            if (request.Mode == SearchChangeRequestMode.Outcoming)
                changeRequests = await _changeRequestRepository.GetUserOutcomingsAsync(currentUser.Id, cancellationToken);
            else
                changeRequests = await _changeRequestRepository.GetUserIncomingsAsync(currentUser.Id, cancellationToken);

            var queueIds = changeRequests.Select(chr => chr.QueueId).Distinct();

            _queueInfo = await GetRecords(queueIds, cancellationToken);

            var res = changeRequests.Select(chr => GetChRDetails(chr, request.Mode)).OrderByDescending(chr => chr.Id).ToList();

            return new GetChangeRequestsQueryResult(res);
        }

        private async Task<Dictionary<int, IEnumerable<Record>>> GetRecords(IEnumerable<int> queueIds, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, IEnumerable<Record>>();

            foreach (var queueId in queueIds)
            {
                var queue = await _queueRepository.GetQueueByIdDetailedAsync(queueId, cancellationToken);
                result.Add(queueId, queue.Records);
            }

            return result;
        }

        public GetChangeRequestQueryResult GetChRDetails(ChangeRequest chr, SearchChangeRequestMode mode)
        {
            string? firstName = null;
            string? lastName = null;

            if (mode == SearchChangeRequestMode.Incoming)
            {
                firstName = chr.UserFromFirstName;
                lastName = chr.UserFromLastName;
            }
            else
            {
                firstName = chr.UserToFirstName;
                lastName = chr.UserToLastName;
            }

            if (chr.Status == RequestStatus.Void || chr.Status == RequestStatus.Declined || chr.Status == RequestStatus.Approved)
            {
                return new GetChangeRequestQueryResult(
                    id: chr.Id,
                    queueId: chr.RecordFrom?.QueueId,
                    queueStartTime: chr.StartTime,
                    subjectInstanceName: chr.SubjectInstanceName,
                    peopleIn: null,
                    currentUserPosition: null,
                    anotherUserPosition: null,
                    userFirstName: firstName,
                    userLastName: lastName,
                    status: chr.Status);
            }

            int? currentUserPosition = null;
            int? anotherUserPosition = null;

            if (mode == SearchChangeRequestMode.Incoming)
            {
                currentUserPosition = GetUserPosition(chr.UserToId, _queueInfo[chr.QueueId]);
                anotherUserPosition = GetUserPosition(chr.UserFromId, _queueInfo[chr.QueueId]);
            }
            else
            {
                anotherUserPosition = GetUserPosition(chr.UserToId, _queueInfo[chr.QueueId]);
                currentUserPosition = GetUserPosition(chr.UserFromId, _queueInfo[chr.QueueId]);
            }

            return new GetChangeRequestQueryResult(
                id: chr.Id,
                queueId: chr.RecordFrom?.QueueId,
                queueStartTime: chr.StartTime,
                subjectInstanceName: chr.SubjectInstanceName,
                peopleIn: chr.RecordFrom!.Queue!.Records.Count,
                currentUserPosition: currentUserPosition,
                anotherUserPosition: anotherUserPosition,
                userFirstName: firstName,
                userLastName: lastName,
                status: chr.Status);
        }

        private int GetUserPosition(int userId, IEnumerable<Record> records)
        {
            var tail = records.SingleOrDefault(r => r.NextRecordId == null);

            if (tail is null)
            {
                return -1;
            }

            var current = tail;
            int counter = records.Count();
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
