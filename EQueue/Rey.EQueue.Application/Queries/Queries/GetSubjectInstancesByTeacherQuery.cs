using MediatR;
using Rey.EQueue.Application.Queries.QueryModels;
using Rey.EQueue.Application.Queries.QueryResults;

namespace Rey.EQueue.Application.Queries.Queries
{
    public class GetSubjectInstancesByTeacherQuery : IRequest<GetSubjectInstancesQueryResult>
    {
        public GetSubjectInstancesByTeacherQuery(int teacherId)
        {
            TeacherId = teacherId;
        }
        public int TeacherId { get; set; }
    }
}
