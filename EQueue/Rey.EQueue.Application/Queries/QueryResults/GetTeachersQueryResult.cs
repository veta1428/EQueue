using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Application.Queries.QueryResults
{
    public class GetTeachersQueryResult
    {
        public GetTeachersQueryResult(IEnumerable<TeacherModel> teachers)
        {
            Teachers = teachers;
        }

        public IEnumerable<TeacherModel> Teachers { get; set; }
    }
}
