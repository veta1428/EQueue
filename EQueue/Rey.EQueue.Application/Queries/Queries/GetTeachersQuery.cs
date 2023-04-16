using MediatR;
using Rey.EQueue.Application.Queries.QueryResults;

namespace Rey.EQueue.Application.Queries.Queries
{
    public class GetTeachersQuery: IRequest<GetTeachersQueryResult>
    {
        public GetTeachersQuery()
        {

        }
    }
}
