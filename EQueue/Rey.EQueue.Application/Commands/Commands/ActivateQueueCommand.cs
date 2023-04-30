using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class ActivateQueueCommand : IRequest
    {
        public ActivateQueueCommand(int queueId) 
        {
            QueueId = queueId;
        }
        public int QueueId { get; set; }
    }
}
