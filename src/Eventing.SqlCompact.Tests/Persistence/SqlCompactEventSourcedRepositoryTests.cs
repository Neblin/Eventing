using Eventing.SqlCompact.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Eventing.SqlCompact.Tests.Persistence
{
    [TestClass]
    public class SqlCompactEventSourcedRepositoryTests
    {
        private SqlCompactEventSourcedRepository sut;

        public SqlCompactEventSourcedRepositoryTests()
        {
            this.sut = new SqlCompactEventSourcedRepository();
        }

        [TestMethod]
        public void GivenNoStreamWhenTryingToGetThenReturnsNull()
        {
            var testAggregate = this.sut.GetAsync<NullAggregate>("category-streamId");

            Assert.IsNull(testAggregate);
        }
    }

    public class NullAggregate : EventSourced { }
}
