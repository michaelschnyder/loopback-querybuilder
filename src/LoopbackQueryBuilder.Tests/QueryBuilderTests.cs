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

            Assert.AreEqual("{ where: { name: 'foo' } }", queryResult.ToString());
        }

        [TestMethod]
        public void Where_Contains_String()
        {
            var queryResult = GetQueryBuilder().Where(car => car.Name.Contains("di"));

            Assert.AreEqual("{ where: { name: { like: '%di%' } } }", queryResult.ToString());
        }

        [TestMethod]
        public void Where_ContainsAndEqual_String()
        {
            var queryResult = GetQueryBuilder().Where(car => car.Name.Contains("foo") && car.Name == "bla");

            Assert.AreEqual("{ where: { and: [ { name: { like: '%foo%' } }, { name: 'bla' } ] } }", queryResult.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Where_NotEquals_String()
        {
            var queryResult = GetQueryBuilder().Where(car => !(car.Name == "foo"));

            Assert.AreEqual("{ where: { name: 'foo' } }", queryResult.ToString());
        }

        [TestMethod]
        public void Where_Equals_Int()
        {
            var queryResult = GetQueryBuilder().Where(car => car.Id == 2);

            Assert.AreEqual("{ where: { id: 2 } }", queryResult.ToString());
        }

        [TestMethod]
        public void Where_Equals_BooleanExplicit()
        {
            var queryResult = GetQueryBuilder().Where(car => car.IsPerfect == true);

            Assert.AreEqual("{ where: { isPerfect: true } }", queryResult.ToString());
        }

        [TestMethod]
        public void Where_Equals_BooleanImplicit()
        {
            var queryResult = GetQueryBuilder().Where(car => car.IsPerfect);

            Assert.AreEqual("{ where: { isPerfect: true } }", queryResult.ToString());
        }

        [TestMethod]
        public void Where_EqualsAnd_String()
        {
            var queryResult = GetQueryBuilder().Where(car => car.Id == 2 && car.Name == "Audi");

            Assert.AreEqual("{ where: { and: [ { id: 2 }, { name: 'Audi' } ] } }", queryResult.ToString());
        }

        [TestMethod]
        public void Take_Int_String()
        {
            var queryResult = GetQueryBuilder().Take(5);

            Assert.AreEqual("{ limit: 5 }", queryResult.ToString());
        }

        [TestMethod]
        public void Where_Equals_Take_Int_String()
        {
            var queryResult = GetQueryBuilder().Where(car => car.Name == "foo").Take(5);

            Assert.AreEqual("{ where: { name: 'foo' }, limit: 5 }", queryResult.ToString());
        }

        [TestMethod]
        public void Skip_Int_String()
        {
            var queryResult = GetQueryBuilder().Skip(5);

            Assert.AreEqual("{ skip: 5 }", queryResult.ToString());
        }

        [TestMethod]
        public void Skip_Int_Take_Int_String()
        {
            var queryResult = GetQueryBuilder().Skip(5).Take(50);

            Assert.AreEqual("{ limit: 50, skip: 5 }", queryResult.ToString());
        }

        [TestMethod]
        public void Where_Equals_Skip_Int_Take_Int_String()
        {
            var queryResult = GetQueryBuilder().Where(car => car.Name == "foo").Skip(5).Take(50);

            Assert.AreEqual("{ where: { name: 'foo' }, limit: 50, skip: 5 }", queryResult.ToString());
        }


        private LoopbackQueryBuilder<Car> GetQueryBuilder()
        {
            var lookbackQueryBuilder = new LoopbackQueryBuilder<Car>();

            lookbackQueryBuilder.SerializationSettings.PropertyEscape = null;
            lookbackQueryBuilder.SerializationSettings.OperationEscape = null;
            lookbackQueryBuilder.SerializationSettings.UnsafeValueEscape = '\'';

            return lookbackQueryBuilder;
        }
    }
}
