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
            XmlSerializer xmlSerialiser = new XmlSerializer(typeof(T));

            StringBuilder builder = new StringBuilder();

            using (EncodedStringWriter stringWriter = new EncodedStringWriter(builder, Encoding.UTF8))
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
                {
                    Indent = false
                };

                using (XmlWriter xmlWriter = XmlTextWriter.Create(stringWriter, xmlWriterSettings))
                {
                    XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();

                    xmlSerializerNamespaces.Add(string.Empty, string.Empty);

                    xmlSerializerNamespaces.Add("", Constants.API_NAMESPACE);

                    xmlSerialiser.Serialize(xmlWriter, obj, xmlSerializerNamespaces);
                }

                return stringWriter.ToString();
            }
        }

        public T Deserialise<T>(string source)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StringReader reader = new StringReader(source))
            {
                return (T)ser.Deserialize(reader);
            }
        }
    }

    internal class EncodedStringWriter : StringWriter
    {
        private Encoding encoding;

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