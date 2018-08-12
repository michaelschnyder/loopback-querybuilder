using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoopbackQueryBuilder.Tests
{
    [TestClass]
    public class OperationSerializationTests
    {
        [TestMethod]
        public void TestMethod2()
        {
            var and = new EqualityOperation("name", "foo");

            Assert.AreEqual("{ name: 'foo' }", and.ToString());
        }
    }
}