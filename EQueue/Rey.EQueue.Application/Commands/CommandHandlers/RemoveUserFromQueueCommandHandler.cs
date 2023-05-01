using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class RemoveUserFromQueueCommandHandler : IRequestHandler<RemoveUserFromQueueCommand>
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IUserAccessor _userAccessor;
        private readonly IChangeRequestRepository _changeRequestRepository;

        public RemoveUserFromQueueCommandHandler(
            IRecordRepository recordRepository,
            IUserAccessor userAccessor,
            IChangeRequestRepository changeRequestRepository)
        {
            _recordRepository = recordRepository;
            _userAccessor = userAccessor;
            _changeRequestRepository = changeRequestRepository;

        }

        public async Task<Unit> Handle(RemoveUserFromQueueCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userAccessor.CurrentUser
                ?? throw new InvalidOperationException("No user in context!");

            var currentRecord = await _recordRepository.GetUserRecordByQueueAsync(currentUser.Id, request.QueueId, cancellationToken)
                ?? throw new InvalidOperationException("User is not in the queue. Cannot remove it");

            var toChangeRequests = currentRecord.ChangeTo.ToList();
            foreach (var changeRequest in toChangeRequests)
            {
                changeRequest.RecordToId = null;
                changeRequest.Status = Core.Enums.RequestStatus.Void;
            }

            _changeRequestRepository.UpdateRange(toChangeRequests);


            var fromChangeRequests = currentRecord.ChangeFrom.ToList();

            foreach (var changeRequest in fromChangeRequests)
            {
                changeRequest.RecordFromId = null;
                changeRequest.Status = Core.Enums.RequestStatus.Void;
            }

            _changeRequestRepository.UpdateRange(fromChangeRequests);

            await _changeRequestRepository.SaveChangesAsync(cancellationToken);



            var prevRecord = await _recordRepository.GetPrevRecordAsync(currentRecord, cancellationToken);

            if (prevRecord is null) // user is the first in queue
            {
                _recordRepository.Delete(currentRecord);
                await _recordRepository.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

            // user is in the middle or is the last

            prevRecord.NextRecordId = currentRecord.NextRecordId;
            _recordRepository.Update(prevRecord);
            _recordRepository.Delete(currentRecord);
            await _recordRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
