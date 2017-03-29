using System;

namespace Eventing
{
    public interface IEventStreamSubscription
    {
        void Start();
        void Stop();
    }

    public interface IEventStreamSubscriber
    {
        IEventStreamSubscription CreateSubscription(string streamName, long? checkpoint, Action<long, object> handler);
    }
}
