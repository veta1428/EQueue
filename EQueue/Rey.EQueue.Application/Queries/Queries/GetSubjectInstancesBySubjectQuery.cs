using MediatR;
using Rey.EQueue.Application.Queries.QueryResults;

namespace Rey.EQueue.Application.Queries.Queries
{
    public class GetSubjectInstancesBySubjectQuery: IRequest<GetSubjectInstancesQueryResult>
    {
        public GetSubjectInstancesBySubjectQuery(int subjectId)
        {
            SubjectId = subjectId;
        }

        public int SubjectId { get; set; }
    }
}
