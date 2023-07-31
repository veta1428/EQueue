using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Application.Services;
using Rey.EQueue.Core.Enums;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    public class DeclineChangeRequestCommandHandler : IRequestHandler<DeclineChangeRequestCommand>
    {
        private readonly IChangeRequestRepository _changeRequestRepository;
        private readonly IRoleManager _roleManager;

        public DeclineChangeRequestCommandHandler(
            IChangeRequestRepository changeRequestRepository,
            IRoleManager roleManager)
        {
            _changeRequestRepository = changeRequestRepository;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(DeclineChangeRequestCommand request, CancellationToken cancellationToken)
        {
            if (!_roleManager.IsUserInGroup())
                throw new InvalidOperationException("No access");

            var chr = await _changeRequestRepository.FindByIdAsync(request.ChangeRequestId, cancellationToken);
            chr.Status = RequestStatus.Declined;
            _changeRequestRepository.Update(chr);
            await _changeRequestRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
