namespace Rey.EQueue.Application.Queries.QueryModels
{
    public class SubjectInstanceDetailedModel
    {
        public int Id { get; init; }

        public TimetableModel? Timetable { get; init; }

        public string? InstanceDescription { get; init; }

        public string InstanceName { get; init; } = null!;

        public IEnumerable<TeacherModel> Teachers { get; init; } = new List<TeacherModel>();

        public string? SubjectName { get; init; }
    }
}
