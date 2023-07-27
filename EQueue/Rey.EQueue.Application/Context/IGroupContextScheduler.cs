namespace Rey.EQueue.Application.Context
{
    public interface IGroupContextScheduler
    {
        Task ExecuteAsync(Func<Task> func, int? groupId, CancellationToken cancellationToken);
    }
}
