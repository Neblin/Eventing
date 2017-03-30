using System;

namespace Eventing
{
    public interface IEventStreamSubscriber
    {
        IEventStreamSubscription CreateSubscription(string streamName, Lazy<long?> lastCheckpoint, Action<long, object> handler);
    }
}
