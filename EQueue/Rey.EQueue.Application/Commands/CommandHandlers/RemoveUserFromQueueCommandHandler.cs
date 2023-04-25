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

        public RemoveUserFromQueueCommandHandler(
            IRecordRepository recordRepository,
            IUserAccessor userAccessor)
        {
            _recordRepository = recordRepository;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(RemoveUserFromQueueCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userAccessor.CurrentUser
                ?? throw new InvalidOperationException("No user in context!");

            var currentRecord = await _recordRepository.GetUserRecordByQueueAsync(currentUser.Id, request.QueueId, cancellationToken)
                ?? throw new InvalidOperationException("User is not in the queue. Cannot remove it");

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
