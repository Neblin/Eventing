using Eventing.Utils;
using EventStore.ClientAPI;
using System;

namespace Eventing.GetEventStore.Subscription
{
    public class EventSubscriber : IEventSubscriber
    {
        private readonly IEventStoreConnection resilientConnection;

        public EventSubscriber(IEventStoreConnection resilientConnection)
        {
            Ensure.NotNull(resilientConnection, nameof(resilientConnection));

            this.resilientConnection = resilientConnection;
        }

        public void StartSubscription(string subscriptionName, string streamName, Action<object> handler)
        {
            throw new NotImplementedException();
        }
    }
}
