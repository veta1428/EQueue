using Rey.EQueue.Application.Context;
using Rey.EQueue.Shared;

namespace Rey.EQueue.Web.Context
{
    public class GroupContextAccessor : IGroupContextAccessor
    {
        private readonly AsyncLocal<GroupContext?> _current = new();

        public GroupContext? Current 
        { 
            get => _current.Value; 
            protected set => _current.Value = value; 
        }

        public IDisposable UseGroupContext(GroupContext? group)
        {
            var store = Current;
            Current = group;
            return new OnDisposeAction(() => Current = store);
        }
    }
}
