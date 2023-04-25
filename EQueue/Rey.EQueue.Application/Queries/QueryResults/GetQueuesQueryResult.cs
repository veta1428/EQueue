using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Application.Queries.QueryResults
{
    public class GetQueuesQueryResult
    {
        public GetQueuesQueryResult(IEnumerable<QueueModel> queues) 
        { 
            Queues = queues;
        }

        public IEnumerable<QueueModel> Queues { get; set; }
    }
}
