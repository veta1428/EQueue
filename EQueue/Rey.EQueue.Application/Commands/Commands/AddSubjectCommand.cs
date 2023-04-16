using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class AddSubjectCommand : IRequest<int>
    {
        public AddSubjectCommand(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string? Description { get; set; }
    }
}

