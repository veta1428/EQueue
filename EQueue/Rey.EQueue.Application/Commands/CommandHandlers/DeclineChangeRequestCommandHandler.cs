using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Enums;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    public class DeclineChangeRequestCommandHandler : IRequestHandler<DeclineChangeRequestCommand>
    {
        private readonly IChangeRequestRepository _changeRequestRepository;

        public DeclineChangeRequestCommandHandler(IChangeRequestRepository changeRequestRepository)
        {
            _changeRequestRepository = changeRequestRepository;
        }

        public async Task<Unit> Handle(DeclineChangeRequestCommand request, CancellationToken cancellationToken)
        {
            var chr = await _changeRequestRepository.FindByIdAsync(request.ChangeRequestId, cancellationToken);
            chr.Status = RequestStatus.Declined;
            _changeRequestRepository.Update(chr);
            await _changeRequestRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
