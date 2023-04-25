namespace Rey.EQueue.Application.Queries.QueryModels
{
    public class RecordModel
    {
        public int RecordId { get; set; }

        public string StudentFirstName { get; set; } = null!;

        public string StudentLastName { get; set; } = null!;

        public DateTime Created { get; set; }

        public int Position { get; set; }

        public bool IsCurrentUser { get; set; } = false;
    }
}
