using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class DeclineChangeRequestCommand : IRequest
    {
        public DeclineChangeRequestCommand(int changeRequestId)
        {
            ChangeRequestId = changeRequestId;
        }

        public int ChangeRequestId { get; set; }
    }
}
