using MediatR;
using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Application.Queries.Queries
{
    public class GetGroupsQuery : IRequest<IEnumerable<GroupModel>>
    {
    }
}
