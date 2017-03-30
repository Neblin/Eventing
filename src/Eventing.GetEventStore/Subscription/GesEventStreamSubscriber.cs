using Eventing.Utils;
using EventStore.ClientAPI;
using System;

namespace Eventing.GetEventStore.Subscription
{
    public class GesEventStreamSubscriber : IEventStreamSubscriber
    {
        private readonly IEventStoreConnection resilientConnection;
        private readonly TimeSpan closeTimeout;

        public GesEventStreamSubscriber(IEventStoreConnection resilientConnection)
            : this(resilientConnection, TimeSpan.FromMinutes(1))
        { }

        public GesEventStreamSubscriber(IEventStoreConnection resilientConnection, TimeSpan closeTimeout)
        {
            Ensure.NotNull(resilientConnection, nameof(resilientConnection));

            this.resilientConnection = resilientConnection;
            this.closeTimeout = closeTimeout;
        }

        public IEventStreamSubscription CreateSubscription(string streamName, Lazy<long?> lastCheckpoint, Action<long, object> handler)
        {
            return new GesEventStreamSubscription(
                this.resilientConnection,
                streamName,
                lastCheckpoint,
                handler,
                this.closeTimeout);
        }
    }
}
