using System;
using System.Collections.Generic;

namespace CharityPlatform.SharedKernel
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }

        private readonly List<Event> _changes = new List<Event>();

        public IReadOnlyCollection<Event> Changes => _changes;

        protected void Apply(Event e)
        {
            ChangeState(e);
            _changes.Add(e);
        }

        protected virtual void ChangeState(Event e)
        {
        }
    }
}
