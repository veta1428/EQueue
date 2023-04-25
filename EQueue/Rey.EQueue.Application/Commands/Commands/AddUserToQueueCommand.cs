using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class AddUserToQueueCommand : IRequest<int>
    {
        public AddUserToQueueCommand(int queueId) 
        {
            QueueId = queueId;
        }
        public int QueueId { get; set; }
    }
}
