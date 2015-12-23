using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace com.esendex.sdk.utilities
{
    internal class XmlSerialiser : ISerialiser
    {
        public string Serialise<T>(T obj)
        {
            var xmlSerialiser = new XmlSerializer(typeof (T));

            var builder = new StringBuilder();

            using (var stringWriter = new EncodedStringWriter(builder, Encoding.UTF8))
            {
                var xmlWriterSettings = new XmlWriterSettings
                {
                    Indent = false
                };

                using (var xmlWriter = XmlTextWriter.Create(stringWriter, xmlWriterSettings))
                {
                    var xmlSerializerNamespaces = new XmlSerializerNamespaces();

                    xmlSerializerNamespaces.Add(string.Empty, string.Empty);

                    xmlSerializerNamespaces.Add("", Constants.API_NAMESPACE);

                    xmlSerialiser.Serialize(xmlWriter, obj, xmlSerializerNamespaces);
                }

                return stringWriter.ToString();
            }
        }

        public T Deserialise<T>(string source)
        {
            var ser = new XmlSerializer(typeof (T));

            using (var reader = new StringReader(source))
            {
                return (T) ser.Deserialize(reader);
            }
        }
    }

    internal class EncodedStringWriter : StringWriter
    {
        private readonly Encoding encoding;

        public EncodedStringWriter(StringBuilder builder, Encoding encoding)
            : base(builder)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }
}