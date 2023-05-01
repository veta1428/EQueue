using Rey.EQueue.Core.Entities;
using Rey.EQueue.Core.Enums;

namespace Rey.EQueue.Application.Queries.QueryResults
{
    public class GetChangeRequestQueryResult
    {
        public GetChangeRequestQueryResult(
            int id, 
            int? queueId,
            DateTime queueStartTime,
            string subjectInstanceName,
            int? peopleIn,
            int? currentUserPosition,
            int? anotherUserPosition,
            string userFirstName,
            string userLastName,
            RequestStatus status) 
        {
            Id = id;
            QueueId = queueId;
            QueueStartTime = queueStartTime;
            SubjectInstanceName = subjectInstanceName;    
            PeopleIn = peopleIn;
            CurrentUserPosition = currentUserPosition;
            AnotherUserPosition = anotherUserPosition;
            UserFirstName = userFirstName;
            UserLastName = userLastName;
            Status = status.ToString();
        }

        public int Id { get; set; }

        public int? QueueId { get; set; }

        public DateTime QueueStartTime { get; set; }

        public string SubjectInstanceName { get; set; }

        public int? PeopleIn { get; set; }

        public int? CurrentUserPosition { get; set; }

        public int? AnotherUserPosition { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string Status { get; set; }
    }
}
