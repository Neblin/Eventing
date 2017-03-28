using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventing
{
    public interface IEventSourced
    {
        string StreamName { get; }
        string StreamId { get; }
        string StreamCategory { get; }
        void Rehydrate(ISnapshot snapshot);
        void Apply(object @event);
        void Emit(object @event);
        ICollection<object> PendingEvents { get; }
        void Clear();
        ISnapshot TakeSnapshot();
    }
}
