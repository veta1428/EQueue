using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class RemoveUserFromQueueCommand : IRequest
    {
        public RemoveUserFromQueueCommand(int queueId) 
        {
            QueueId = queueId;
        }

        public int QueueId { get; set; }
    }
}
