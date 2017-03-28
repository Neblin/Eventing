using System;
using System.Collections.Generic;

namespace Eventing
{
    public abstract class EventSourced : IEventSourced
    {
        public string StreamName => throw new NotImplementedException();

        public string StreamId => throw new NotImplementedException();

        public string StreamCategory => StreamCategoryAttribute.GetCategory(this.GetType());

        public ICollection<object> PendingEvents => throw new NotImplementedException();

        public void Apply(object @event)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Emit(object @event)
        {
            throw new NotImplementedException();
        }

        public void Rehydrate(ISnapshot snapshot)
        {
            throw new NotImplementedException();
        }

        public ISnapshot TakeSnapshot()
        {
            throw new NotImplementedException();
        }
    }
}
