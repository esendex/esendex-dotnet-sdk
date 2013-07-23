using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using com.esendex.sdk.messaging;
using com.esendex.sdk.test.messaging.schemas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.esendex.sdk.test.messaging
{
    [TestClass]
    public class MessageSchemaTests
    {
        [TestMethod]
        [Ignore]
        public void SerialiseSmsMessage()
        {
            // Arrange
            try
            {
                SmsMessage smsMessage = new SmsMessage();

                string xml = "";

                // Act - Assert
                XmlSchema schema = GetEmbeddedSchema(MessageSchemas.DispatcherRequestSchemaName);
                
                XmlReaderSettings settings = new XmlReaderSettings()
                {
                    ValidationType = ValidationType.Schema,
                    ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
                };

                settings.ValidationEventHandler += new ValidationEventHandler(settings_ValidationEventHandler);
                settings.Schemas.Add(schema);

                XmlReader reader = XmlReader.Create(new StringReader(xml), settings);

                while (reader.Read()) { }

            }
            catch (XmlException)
            {
                Assert.Fail();
            }
        }

        void settings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            throw new XmlSchemaValidationException(e.Message);
        }

        public static XmlSchema GetEmbeddedSchema(string schemaName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string schemaLocation = string.Format("{0}.{1}", assembly.GetName().Name, schemaName);

            Stream stream = assembly.GetManifestResourceStream(schemaLocation);

            return XmlSchema.Read(stream, null);
        }
    }
}
