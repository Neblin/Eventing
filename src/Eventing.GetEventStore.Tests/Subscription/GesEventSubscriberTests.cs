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
    public class GesEventSubscriberTests
    {
        private ClusterVNode node;
        private GesEventStreamSubscriber sut;

        public GesEventSubscriberTests()
        {
            var nodeBuilder = EmbeddedVNodeBuilder
                                .AsSingleNode()
                                .RunInMemory();

            this.node = nodeBuilder.Build();
            this.node.StartAndWaitUntilReady().Wait();

            this.sut = new GesEventStreamSubscriber(EmbeddedEventStoreConnection.Create(this.node));
        }

        [TestMethod]
        public void ShouldSubscribeFromTheBeginingOfAStream()
        {
            var stream = "testStream";
            this.WriteEvent(stream, 10);

            var processedCount = 0;

            this.sut.CreateSubscription("testStream", null, (c, e) => processedCount++)
                .Start();

            Wait.For(TimeSpan.FromSeconds(5), () => processedCount >= 10, () => throw new TimeoutException($"Total procesado: {processedCount}"));
        }

        [TestMethod]
        public void CanStopARunningSubscription()
        {
            var stream = "testStream";
            this.WriteEvent(stream, 10);

            var processedCount = 0;

            var sub = this.sut.CreateSubscription("testStream", null, (c, e) => processedCount++);
            sub.Start();
            sub.Stop();            
        }

        private void WriteEvent(string stream, int quantity = 1)
        {
            using (var conn = EmbeddedEventStoreConnection.Create(this.node))
            {
                conn.ConnectAsync().Wait();
                for (int i = 0; i < quantity; i++)
                {
                    conn.AppendToStreamAsync(stream, ExpectedVersion.Any,
                            new EventData(Guid.NewGuid(), "testEventType", true,
                                Encoding.UTF8.GetBytes(@"{'foo':'bar'}"), null)).Wait(); 
                }
            }
        }
    }
}
