using Eventing.GetEventStore.Subscription;
using Eventing.Utils;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Embedded;
using EventStore.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace Eventing.GetEventStore.Tests.Subscription
{
    [TestClass]
    public class EventSubscriberTests
    {
        private ClusterVNode node;
        private EventSubscriber sut;

        public EventSubscriberTests()
        {
            var nodeBuilder = EmbeddedVNodeBuilder
                                .AsSingleNode()
                                .RunInMemory();

            this.node = nodeBuilder.Build();
            this.node.StartAndWaitUntilReady().Wait();

            this.sut = new EventSubscriber(null);
        }

        [TestMethod]
        public void ShouldSubscribeFromTheBeginingOfAStream()
        {
            var stream = "testStream";
            this.WriteEvent(stream, 10);

            var processedCount = 0;

            this.sut.StartSubscription("newSub", stream, e => 
            {
                processedCount++;
            });

            Wait.For(TimeSpan.FromSeconds(10), () => processedCount >= 10, () => throw new TimeoutException());
        }

        private void WriteEvent(string stream, int quantity = 1)
        {
            using (var conn = EmbeddedEventStoreConnection.Create(this.node))
            {
                conn.ConnectAsync().Wait();
                for (int i = 0; i < 1; i++)
                {
                    conn.AppendToStreamAsync(stream, ExpectedVersion.Any,
                            new EventData(Guid.NewGuid(), "testEventType", true,
                                Encoding.UTF8.GetBytes(@"{'foo':'bar'}"), null)).Wait(); 
                }
            }
        }
    }
}
