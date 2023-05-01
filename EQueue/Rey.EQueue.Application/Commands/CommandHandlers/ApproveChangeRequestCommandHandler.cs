using MediatR;
using Microsoft.EntityFrameworkCore;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Enums;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    public class ApproveChangeRequestCommandHandler : IRequestHandler<ApproveChangeRequestCommand>
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IChangeRequestRepository _changeRequestRepository;

        public ApproveChangeRequestCommandHandler(IRecordRepository recordRepository, IChangeRequestRepository changeRequestRepository)
        {
            _recordRepository = recordRepository;
            _changeRequestRepository = changeRequestRepository;
        }

        public async Task<Unit> Handle(ApproveChangeRequestCommand request, CancellationToken cancellationToken)
        {
            var changeRequest = await _changeRequestRepository.FindByIdAsync(request.ChangeRequestId, cancellationToken);

            if (changeRequest.Status != RequestStatus.Pending)
                throw new InvalidOperationException($"Change Request should be in Status {nameof(RequestStatus.Pending)} to be approved.");

            if (changeRequest.RecordFromId is null || changeRequest.RecordToId is null)
                throw new InvalidOperationException("Change Request between unknown records");

            // another person record (for this person we are approving this change request)
            // need to void all incoming and outcoming requests

            var record = await _recordRepository
                .GetQuery()
                .Where(r => r.Id == changeRequest.RecordFromId)
                .Include(record => record.ChangeFrom)
                .Include(record => record.ChangeTo)
                .SingleAsync(cancellationToken);

            foreach(var chr in record.ChangeTo.Where(c => c.Status == RequestStatus.Pending))
            {
                chr.Status = RequestStatus.Void;
            }

            foreach (var chr in record.ChangeFrom.Where(c => c.Status == RequestStatus.Pending))
            {
                chr.Status = RequestStatus.Void;
            }

            // approver record
            // need to decline other requests and void outcoming requests
            var approverRecord = await _recordRepository
                .GetQuery()
                .Where(r => r.Id == changeRequest.RecordToId)
                .Include(record => record.ChangeFrom)
                .Include(record => record.ChangeTo)
                .SingleAsync(cancellationToken);

            foreach (var chr in approverRecord.ChangeTo.Where(c => c.Status == RequestStatus.Pending))
            {
                chr.Status = RequestStatus.Void;
            }

            foreach (var chr in record.ChangeFrom.Where(c => c.Status == RequestStatus.Pending))
            {
                chr.Status = RequestStatus.Void;
            }

            var recordTo = await _recordRepository.FindByIdAsync(changeRequest.RecordToId.Value, cancellationToken);
            var recordFrom = await _recordRepository.FindByIdAsync(changeRequest.RecordFromId.Value, cancellationToken);

            var temp = recordTo.UserId;
            recordTo.UserId = recordFrom.UserId;
            recordFrom.UserId = temp;

            changeRequest.Status = RequestStatus.Approved;

            _changeRequestRepository.UpdateRange(record.ChangeTo);
            _changeRequestRepository.UpdateRange(record.ChangeFrom);

            _recordRepository.Update(recordTo);
            _recordRepository.Update(recordFrom);

            await _changeRequestRepository.SaveChangesAsync(cancellationToken);
            await _changeRequestRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
