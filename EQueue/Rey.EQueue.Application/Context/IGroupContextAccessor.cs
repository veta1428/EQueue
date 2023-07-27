namespace Rey.EQueue.Application.Context
{
    public interface IGroupContextAccessor
    {
        GroupContext? Current { get; }

        IDisposable UseGroupContext(GroupContext? group);
    }
}
