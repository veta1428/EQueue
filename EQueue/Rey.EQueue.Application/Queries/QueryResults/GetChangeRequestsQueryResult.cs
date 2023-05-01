namespace Rey.EQueue.Application.Queries.QueryResults
{
    public class GetChangeRequestsQueryResult
    {
        public GetChangeRequestsQueryResult(IEnumerable<GetChangeRequestQueryResult> changeRequests) 
        {
            ChangeRequests = changeRequests;
        }

        public IEnumerable<GetChangeRequestQueryResult> ChangeRequests { get; set; }
    }
}
