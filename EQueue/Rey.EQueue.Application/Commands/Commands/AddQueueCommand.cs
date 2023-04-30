using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class AddQueueCommand : IRequest<int>
    {
        public AddQueueCommand() { }

        public int SubjectInstanceId { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public string? Description { get; set; }
    }
}
