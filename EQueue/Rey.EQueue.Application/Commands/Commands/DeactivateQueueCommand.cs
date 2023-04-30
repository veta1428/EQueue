using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class DeactivateQueueCommand : IRequest
    {
        public DeactivateQueueCommand(int queueId)
        {
            QueueId = queueId;
        }

        public int QueueId { get; set; }
    }
}
