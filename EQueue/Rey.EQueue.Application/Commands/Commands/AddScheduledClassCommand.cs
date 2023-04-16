using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    internal class AddScheduledClassCommand : IRequest<int>
    {
        public int Duration { get; set; }

        public int SubjectInstanceId { get; set; }

        public string? Description { get; set; }

        public DateTime StartTime { get; set; }
    }
}
