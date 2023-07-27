namespace Rey.EQueue.Shared
{
    public class OnDisposeAction : IDisposable
    {
        private bool _disposed = false;
        private Action _action;

        public OnDisposeAction(Action action)
        {
            _action = action;   
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                _action();
                _disposed = true;
            }
        }
    }
}
