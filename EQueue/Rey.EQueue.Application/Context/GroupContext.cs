namespace Rey.EQueue.Application.Context
{
    public class GroupContext
    {
        public GroupContext(int groupId)
        {
            GroupId = groupId;
        }

        public int GroupId { get; set; }
    }
}
