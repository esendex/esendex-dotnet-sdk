using System;
using System.Xml.Serialization;
using com.esendex.sdk.utilities;
using NUnit.Framework;

namespace com.esendex.sdk.test.utilities
{
    [TestFixture]
    public class XmlSerialiserTests
    {
        // Model Context to represent the following XML
        // <ModelContext xmlns:ns="http://api.esendex.com/ns/" Id="{id}"><Value>{value}</Value></ModelContext>
        [XmlRoot(Namespace = "http://api.esendex.com/ns/")]
        public class ModelContext
        {
            [XmlAttribute]
            public Guid Id { get; set; }

            [XmlElement]
            public int Value { get; set; }
        }

        private ISerialiser serialiser;

        [SetUp]
        public void TestInitialize()
        {
            serialiser = new XmlSerialiser();
        }

        [Test]
        public void Serialise_WithModelContext_ReturnsExpectedXml()
        {
            // Arrange
            var expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ModelContext Id=\"fe715b9e-facc-4353-bcb1-bec1b313423a\" xmlns=\"http://api.esendex.com/ns/\"><Value>5</Value></ModelContext>";

            var model = new ModelContext
            {
                Id = new Guid("FE715B9E-FACC-4353-BCB1-BEC1B313423A"),
                Value = 5
            };

            // Act
            var actualXml = serialiser.Serialise(model);

            // Assert
            Assert.AreEqual(expectedXml, actualXml);
        }

        [Test]
        public void Deserialise_WithXml_ReturnsExpectedModel()
        {
            // Arrange
            var expectedModel = new ModelContext
            {
                Id = new Guid("FE715B9E-FACC-4353-BCB1-BEC1B313423A"),
                Value = 5
            };

            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ModelContext xmlns=\"http://api.esendex.com/ns/\" Id=\"fe715b9e-facc-4353-bcb1-bec1b313423a\"><Value>5</Value></ModelContext>";

            // Act
            var actualModel = serialiser.Deserialise<ModelContext>(xml);

            // Assert
            Assert.IsNotNull(actualModel);
            Assert.AreEqual(actualModel.Id, actualModel.Id);
            Assert.AreEqual(actualModel.Value, actualModel.Value);
        }
    }
}