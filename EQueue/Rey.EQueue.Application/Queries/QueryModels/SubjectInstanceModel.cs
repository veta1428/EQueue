namespace Rey.EQueue.Application.Queries.QueryModels
{
    public class SubjectInstanceModel
    {
        public int Id { get; init; }

        public List<string> Timetable { get; init; } = new List<string>();

        public string? InstanceDescription { get; init; }

        public string InstanceName { get; init; } = null!;
    }
}
