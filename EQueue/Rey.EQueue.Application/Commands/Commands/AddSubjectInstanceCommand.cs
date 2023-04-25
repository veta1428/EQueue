using MediatR;

namespace Rey.EQueue.Application.Commands.Commands
{
    public class AddSubjectInstanceCommand : IRequest<int>
    {
        public AddSubjectInstanceCommand(
            string name, 
            string? description, 
            int subjectId, 
            IEnumerable<int> teacherIds)
        {
            Name = name;
            Description = description;
            SubjectId = subjectId;
            TeacherIds = teacherIds;
        }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int SubjectId { get; set; }

        public IEnumerable<int> TeacherIds { get; set; } = new List<int>();
    }
}
