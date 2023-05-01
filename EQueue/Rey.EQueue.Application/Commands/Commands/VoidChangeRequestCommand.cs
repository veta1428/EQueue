using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class VoidChangeRequestCommand : IRequest
    {
        public VoidChangeRequestCommand(int changeRequestId)
        {
            ChangeRequestId = changeRequestId;
        }

        public int ChangeRequestId { get; set; }
    }
}
