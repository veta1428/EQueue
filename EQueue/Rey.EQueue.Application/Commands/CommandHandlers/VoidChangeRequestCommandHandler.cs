using MediatR;
using Rey.EQueue.Application.Commands.Commands;
using Rey.EQueue.Application.Repositories;
using Rey.EQueue.Core.Enums;

namespace Rey.EQueue.Application.Commands.CommandHandlers
{
    internal class VoidChangeRequestCommandHandler : IRequestHandler<VoidChangeRequestCommand>
    {
        private readonly IChangeRequestRepository _changeRequestRepository;

        public VoidChangeRequestCommandHandler(IChangeRequestRepository changeRequestRepository)
        {
            _changeRequestRepository = changeRequestRepository;
        }

        public async Task<Unit> Handle(VoidChangeRequestCommand request, CancellationToken cancellationToken)
        {
            var chr = await _changeRequestRepository.FindByIdAsync(request.ChangeRequestId, cancellationToken);
            chr.Status = RequestStatus.Void;
            _changeRequestRepository.Update(chr);
            await _changeRequestRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
