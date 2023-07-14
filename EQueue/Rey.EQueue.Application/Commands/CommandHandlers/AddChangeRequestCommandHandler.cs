using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;
using Rey.EQueue.Core.Enums;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    public class AddChangeRequestCommandHandler : IRequestHandler<AddChangeRequestCommand, int>
    {
        private readonly IChangeRequestRepository _changeRequestRepository;
        private readonly IUserAccessor _userAccessor;
        private readonly IRecordRepository _recordRepository;
        private readonly IQueueRepository _queueRepository;
        private readonly IUserRepository _userRepository;

        public AddChangeRequestCommandHandler(
            IChangeRequestRepository changeRequestRepository,
            IUserAccessor userAccessor,
            IRecordRepository recordRepository,
            IQueueRepository queueRepository,
            IUserRepository userRepository)
        {
            _changeRequestRepository = changeRequestRepository;
            _userAccessor = userAccessor;
            _recordRepository = recordRepository;
            _queueRepository = queueRepository;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(AddChangeRequestCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userAccessor.CurrentUser ?? throw new InvalidOperationException();
            var currentUserRecord = await _recordRepository.GetUserRecordByQueueAsync(currentUser.Id, request.QueueId, cancellationToken);


            var queue = await _queueRepository.GetQueueByIdDetailedAsync(currentUserRecord.QueueId, cancellationToken);
            var record = await _recordRepository.FindByIdAsync(request.RecordId, cancellationToken);
            var user = await _userRepository.FindByIdAsync(record.UserId, cancellationToken);

            var changeRequest = new ChangeRequest()
            {
                RecordFromId = currentUserRecord.Id,
                RecordToId = request.RecordId,
                Created = DateTime.UtcNow,
                Status = RequestStatus.Pending,
                ScheduledClassStartTime = queue.ScheduledClass!.StartTime,
                SubjectInstanceName = queue.ScheduledClass.SubjectInstance.Name,
                UserFromId = currentUser.Id,
                UserToId = user.Id,
                QueueId = queue.Id,
            };

            _changeRequestRepository.Add(changeRequest);
            await _changeRequestRepository.SaveChangesAsync(cancellationToken);

            return currentUserRecord.Id;
        }
    }
}
