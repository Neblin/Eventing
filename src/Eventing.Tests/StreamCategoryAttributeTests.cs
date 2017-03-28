using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Eventing.Tests
{
    [TestClass]
    public class StreamCategoryAttributeTests
    {
        [TestMethod]
        public void CanGetFromType()
        {
            var category = StreamCategoryAttribute.GetCategory(typeof(CategoryAggregate));

            Assert.IsNotNull(category);
            Assert.AreEqual("test.category", category);
        }

        [TestMethod]
        public void CanGetCategoryFromANewInstance()
        {
            var instance = new CategoryAggregate();
            var category = instance.StreamCategory;

            Assert.IsNotNull(category);
            Assert.AreEqual("test.category", category);
        }

        [TestMethod]
        public void IfNoCategoryThenReturnsNullFromType()
        {
            var category = StreamCategoryAttribute.GetCategory(typeof(NoCategoryAggregate));

            Assert.IsNull(category);
        }

        [TestMethod]
        public void IfNoCategoryThenReturnsNullFromInstance()
        {
            var instance = new NoCategoryAggregate();
            var category = instance.StreamCategory;

            Assert.IsNull(category);
        }
    }

    [StreamCategory("test.category")]
    internal class CategoryAggregate : EventSourced
    { }

    internal class NoCategoryAggregate : EventSourced { }
}
