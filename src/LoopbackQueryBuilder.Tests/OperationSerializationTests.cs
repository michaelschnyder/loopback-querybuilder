using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoopbackQueryBuilder.Tests
{
    [TestClass]
    public class OperationSerializationTests
    {
        [TestMethod]
        public void Equality_DefaultSerialization_Renders()
        {
            var defaultSerializationSettings = new SerializationSettings();

            var operation = new EqualityOperation(defaultSerializationSettings)
            {
                ColumnName = "name",
                Value = "foo"
            };

            Assert.AreEqual("{ \"name\": \"foo\" }", operation.ToString());
        }

        [TestMethod]
        public void Equality_NoPropertyEscape_Renders()
        {
            var serializationSettings = new SerializationSettings();
            serializationSettings.PropertyEscape = null;

            var operation = new EqualityOperation(serializationSettings)
            {
                ColumnName = "name",
                Value = "foo"
            };

            Assert.AreEqual("{ name: \"foo\" }", operation.ToString());
        }

        [TestMethod]
        public void Equality_NoUnsafeValueEscape_Renders()
        {
            var serializationSettings = new SerializationSettings();
            serializationSettings.UnsafeValueEscape = null;

            var operation = new EqualityOperation(serializationSettings)
            {
                ColumnName = "name",
                Value = "foo"
            };

            Assert.AreEqual("{ \"name\": foo }", operation.ToString());
        }
    }
}