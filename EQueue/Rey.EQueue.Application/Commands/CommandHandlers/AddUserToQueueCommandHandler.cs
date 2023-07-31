using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Entities;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    public class AddUserToQueueCommandHandler : IRequestHandler<AddUserToQueueCommand, int>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IRecordRepository _recordRepository;
        private readonly IRoleManager _roleManager;

        public AddUserToQueueCommandHandler(
            IUserAccessor userAccessor,
            IRecordRepository recordRepository,
            IRoleManager roleManager)
        {
            _userAccessor = userAccessor;
            _recordRepository = recordRepository;
            _roleManager = roleManager;
        }

        public async Task<int> Handle(AddUserToQueueCommand request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsUserInGroup())
                throw new InvalidOperationException("No access");

            var user = _userAccessor.CurrentUser 
                ?? throw new InvalidOperationException();

            var lastRecord = await _recordRepository.GetLastRecordByQueueIdAsync(request.QueueId, cancellationToken);

            var record = new Record() { 
                QueueId = request.QueueId, 
                NextRecordId = null,
                CreationDate = DateTime.UtcNow,
                User = user,
            };
            _recordRepository.Add(record);
            await _recordRepository.SaveChangesAsync(cancellationToken);

            if (lastRecord is not null)
            {
                lastRecord.NextRecordId = record.Id;
                _recordRepository.Update(lastRecord);
                await _recordRepository.SaveChangesAsync(cancellationToken);
            }

            return record.Id;
        }
    }
}
