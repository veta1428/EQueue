using MediatR;
using Rey.EQueue.Application.Queries.QueryResults;

namespace Rey.EQueue.Application.Queries.Queries
{
    public class GetQueuesQuery : IRequest<GetQueuesQueryResult>
    {
        public GetQueuesQuery(QueueSearchMode mode)
        {
            Mode = mode;
        }

        public QueueSearchMode Mode { get; set; }
    }

    public enum QueueSearchMode
    {
        Active = 1,
        Inactive = 2,
    }
}
