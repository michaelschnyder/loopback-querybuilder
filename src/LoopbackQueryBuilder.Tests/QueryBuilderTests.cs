using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoopbackQueryBuilder.Tests
{
    [TestClass]
    public class QueryBuilderTests
    {
        [TestMethod]
        public void Where_Equals_String()
        {
            var queryResult = GetQueryBuilder().Where(car => car.Name == "foo");

            Assert.AreEqual("{ where: { name: 'foo' } }", queryResult);
        }

        [TestMethod]
        public void Where_Contains_String()
        {
            var queryResult = GetQueryBuilder().Where(car => car.Name.Contains("di"));

            Assert.AreEqual("{ where: { name: { 'like': '%di%' } } }", queryResult);
        }

        [TestMethod]
        public void Where_ContainsAndEqual_String()
        {
            var queryResult = GetQueryBuilder().Where(car => car.Name.Contains("foo") && car.Name == "bla");

            Assert.AreEqual("{ where: { and: [ { name: { 'like': '%foo%' } }, { name: 'bla' } ] } }", queryResult);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Where_NotEquals_String()
        {
            var queryResult = GetQueryBuilder().Where(car => !(car.Name == "foo"));

            Assert.AreEqual("{ where: { name: 'foo' } }", queryResult);
        }

        [TestMethod]
        public void Where_Equals_Int()
        {
            var queryResult = GetQueryBuilder().Where(car => car.Id == 2);

            Assert.AreEqual("{ where: { id: 2 } }", queryResult);
        }

        [TestMethod]
        public void Where_EqualsAnd_String()
        {
            var queryResult = GetQueryBuilder().Where(car => car.Id == 2 && car.Name == "Audi");

            Assert.AreEqual("{ where: { and: [ { id: 2 }, { name: 'Audi' } ] } }", queryResult);
        }

        private LookbackQueryBuilder<Car> GetQueryBuilder()
        {
            return new LookbackQueryBuilder<Car>();
        }
    }
}
