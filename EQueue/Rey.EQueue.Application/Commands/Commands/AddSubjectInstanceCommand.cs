using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class AddSubjectInstanceCommand : IRequest<int>
    {
        public AddSubjectInstanceCommand(
            string name, 
            string? description, 
            int subjectId, 
            IEnumerable<int> teachers)
        {
            Name = name;
            Description = description;
            SubjectId = subjectId;
            Teachers = teachers;
        }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int SubjectId { get; set; }

        public IEnumerable<int> Teachers { get; set; } = new List<int>();
    }
}
