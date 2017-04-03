using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using System;
using System.Net;
using System.Threading;

namespace SubscriptionPlayground
{
    public class Program
    {
        static IEventStoreConnection _connection;
        static EventStoreCatchUpSubscription _subscription;

        static void Main(string[] args)
        {
            var sb = ConnectionSettings.Create()
                .KeepReconnecting()
                .KeepRetrying();

            sb.SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));
            _connection = EventStoreConnection.Create(sb.Build(), new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1113));

            var prefix = "Connection Event:";
            _connection.Closed += (_, __) => Console.WriteLine($"{prefix} Closed");
            _connection.Connected += (_, __) => Console.WriteLine($"{prefix} Connected");
            _connection.Disconnected += (_, __) => Console.WriteLine($"{prefix} Disconected");
            _connection.Reconnecting += (_, __) => Console.WriteLine($"{prefix} Reconnecting");
            _connection.ErrorOccurred += (_, __) => Console.WriteLine($"{prefix} Error ocurred");

            _connection.ConnectAsync()
                .Wait();

            Subscribe();
            Console.WriteLine("Running. Press any key to exit...");
            Console.ReadKey(true);
        }

        private static void Subscribe()
        {
            Console.WriteLine("Subscribing to all events");
            _subscription = _connection.SubscribeToStreamFrom("$stats-127.0.0.1:2113", null, CatchUpSubscriptionSettings.Default,
                EventAppeared,
                LiveProcessingStarted,
                SubscriptionDropped);
        }

        private static void LiveProcessingStarted(EventStoreCatchUpSubscription obj)
        {
            Console.WriteLine($"{DateTime.UtcNow}: Live processing started on managed thread { Thread.CurrentThread.ManagedThreadId}"); 
        }

        private static void EventAppeared(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent)
        {
            Console.WriteLine($"{DateTime.UtcNow}: Received event { resolvedEvent.OriginalEventNumber} ({ resolvedEvent.OriginalEvent.EventType}) on managed thread { Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(TimeSpan.FromSeconds(10));
        }

        private static void SubscriptionDropped(EventStoreCatchUpSubscription subscription, SubscriptionDropReason reason, Exception ex)
        {
            Console.WriteLine($"Subscription dropped with reason { reason}. Resubscribing..."); 
            _subscription.Stop();
            Subscribe();
        }
    }
}
