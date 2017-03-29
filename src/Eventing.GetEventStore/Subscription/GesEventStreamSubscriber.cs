using Eventing.Utils;
using EventStore.ClientAPI;
using System;

namespace Eventing.GetEventStore.Subscription
{
    public class GesEventStreamSubscriber : IEventStreamSubscriber
    {
        private readonly IEventStoreConnection resilientConnection;
        private readonly TimeSpan stopTimeout;

        public GesEventStreamSubscriber(IEventStoreConnection resilientConnection)
            : this(resilientConnection, TimeSpan.FromMinutes(1))
        { }

        public GesEventStreamSubscriber(IEventStoreConnection resilientConnection, TimeSpan stopTimeout)
        {
            Ensure.NotNull(resilientConnection, nameof(resilientConnection));

            this.resilientConnection = resilientConnection;
            this.stopTimeout = stopTimeout;
        }

        public IEventStreamSubscription CreateSubscription(string streamName, long? checkpoint, Action<long, object> handler)
        {
            return new GesEventStreamSubscription(this.resilientConnection, streamName, checkpoint, handler, this.stopTimeout);
        }
    }
}



