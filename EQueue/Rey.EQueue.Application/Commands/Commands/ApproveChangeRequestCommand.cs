using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class ApproveChangeRequestCommand : IRequest
    {
        public ApproveChangeRequestCommand(int changeRequestId)
        {

            ChangeRequestId = changeRequestId;
        }

        public int ChangeRequestId { get; set; }
    }
}
