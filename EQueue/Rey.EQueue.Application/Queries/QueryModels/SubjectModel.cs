namespace Rey.EQueue.Application.Queries.QueryModels
{
    public class SubjectModel
    {
        public SubjectModel(int id, string name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
