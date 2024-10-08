using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RealEstate.Core.Libs
{
    public static class XmlHelper
    {
        public static string SerializeToXml<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        public static T DeserializeFromXml<T>(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringReader = new StringReader(xml))
            {
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }
    }
}
