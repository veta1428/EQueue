using MediatR;
using Rey.EQueue.Application.Queries.QueryResults;

namespace Rey.EQueue.Application.Queries.Queries
{
    public class GetChangeRequestsQuery : IRequest<GetChangeRequestsQueryResult>
    {
        public GetChangeRequestsQuery(SearchChangeRequestMode mode)
        {
            Mode = mode;
        }

        public SearchChangeRequestMode Mode { get; }
    }

    public enum SearchChangeRequestMode
    {
        Incoming = 1,
        Outcoming = 2
    }
}
