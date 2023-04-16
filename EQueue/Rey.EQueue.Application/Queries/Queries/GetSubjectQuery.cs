using MediatR;
using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Application.Queries.Queries
{
    public class GetSubjectQuery : IRequest<SubjectModel>
    {
        public GetSubjectQuery(int subjectId)
        {
            SubjectId = subjectId;
        }

        public int SubjectId { get; set; }
    }
}
