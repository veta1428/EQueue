using MediatR;
using Rey.EQueue.Application.Queries.QueryResults;

namespace Rey.EQueue.Application.Queries.Queries
{
    public class GetRecordsByQueueQuery : IRequest<GetRecordsByQueueQueryResult>
    {
        public GetRecordsByQueueQuery(int queueId)
        {
            QueueId = queueId;
        }

        public int QueueId { get; set; }
    }
}
