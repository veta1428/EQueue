namespace Rey.EQueue.Shared
{
    public class TaskScheduler
    {
        private static TaskScheduler? _instance = null;

        private List<Timer> timers = new List<Timer>();

        private TaskScheduler() { }

        public static TaskScheduler Instance 
        => _instance ?? (_instance = new TaskScheduler());

        public void RunAndScheduleTask(double intervalInDays, Func<Task> task)
        {
            var timer = new Timer(async x =>
            {
                await task();
            }, null, TimeSpan.FromSeconds(1), TimeSpan.FromDays(intervalInDays));

            timers.Add(timer);
        }
    }
}
