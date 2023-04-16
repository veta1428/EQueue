using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Application.Queries.QueryResults
{
    public class GetSubjectsQueryResult
    {
        public GetSubjectsQueryResult(IEnumerable<SubjectModel> subjects)
        {
            Subjects = subjects;
        }

        public IEnumerable<SubjectModel> Subjects { get; set; }
    }
}
