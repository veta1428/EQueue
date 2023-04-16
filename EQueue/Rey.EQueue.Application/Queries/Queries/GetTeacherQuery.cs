using MediatR;
using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Application.Queries.Queries
{
    public class GetTeacherQuery : IRequest<TeacherModel>
    {
        public GetTeacherQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
