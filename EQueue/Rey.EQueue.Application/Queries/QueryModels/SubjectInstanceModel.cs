namespace Rey.EQueue.Application.Queries.QueryModels
{
    public class SubjectInstanceModel
    {
        public int Id { get; init; }

        public IEnumerable<ClassModel> Classes { get; init; } = new List<ClassModel>();

        public string? InstanceDescription { get; init; }

        public string InstanceName { get; init; } = null!;
    }
}
