using System;

namespace Eventing
{
    public interface IEventSubscriber
    {
        void StartSubscription(string subscriptionName, string streamName, Action<object> handler);
    }
}
