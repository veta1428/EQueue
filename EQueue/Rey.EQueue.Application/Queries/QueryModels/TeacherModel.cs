namespace Rey.EQueue.Application.Queries.QueryModels
{
    public class TeacherModel
    {
        public TeacherModel(
            int id,
            string firstName,
            string lastName,
            string? middleName = null,
            string? description = null,
            string? note = null)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Description = description;
            Note = note;
        }
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public string? Description { get; set; }

        public string? Note { get; set; }
    }
}
