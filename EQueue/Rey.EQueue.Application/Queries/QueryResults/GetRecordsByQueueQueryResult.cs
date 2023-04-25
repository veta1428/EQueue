using Rey.EQueue.Application.Queries.QueryModels;

namespace Rey.EQueue.Application.Queries.QueryResults
{
    public class GetRecordsByQueueQueryResult
    {
        public GetRecordsByQueueQueryResult(
            int queueId, 
            string subjectInstanceName, 
            DateTime startTime, 
            IEnumerable<RecordModel> records) 
        { 
            QueueId = queueId;
            SubjectInstanceName = subjectInstanceName;
            StartTime = startTime;          
            Records = records;
        }

        public int QueueId { get; set; }

        public string SubjectInstanceName { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public IEnumerable<RecordModel> Records { get; set; }
    }
}
