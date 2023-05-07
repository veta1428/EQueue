using MediatR;
using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Application.Queries.Queries
{
    public class GetSubjectInstanceQuery : IRequest<SubjectInstanceDetailedModel>
    {
        public GetSubjectInstanceQuery(int subjectInstanceId)
        {
            SubjectInstanceId = subjectInstanceId;
        }

        public int SubjectInstanceId { get; set; }
    }
}
