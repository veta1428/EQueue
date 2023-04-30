namespace Rey.EQueue.Application.Queries.QueryModels
{
    public class QueueModel
    {
        public int Id { get; set; }

        public string SubjectInstanceName { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public int PeopleIn { get; set; }

        public int? CurrentUserPosition { get; set; }  
        
        public bool IsActive { get; set; }
    }
}
