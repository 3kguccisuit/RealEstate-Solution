using Serilog;
using System.Xml.Serialization;

namespace RealEstateDAL.Libs
{
    public static class XmlHelper
    {
        public static string SerializeToXml<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            try
            {
                using (var stringWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(stringWriter, obj);
                    return stringWriter.ToString();
                }
            }
            catch (InvalidOperationException ex)
            {
                // Log the error or display a message to the user
                Log.Information("Failed to serialize object to XML.", ex);
                throw new InvalidOperationException("An error occurred during XML serialization.", ex);
            }
            catch (Exception ex)
            {
                // Catch any other unexpected exceptions
                Log.Information("An unexpected error occurred during XML serialization.", ex);
                throw new Exception("An unexpected error occurred during XML serialization.", ex);
            }
        }

        public static T DeserializeFromXml<T>(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            try
            {
                using (var stringReader = new StringReader(xml))
                {
                    return (T)xmlSerializer.Deserialize(stringReader);
                }
            }
            catch (InvalidOperationException ex)
            {
                // Log the error
                Log.Information("Failed to deserialize XML to object.", ex);
                // Throw a more specific exception to be caught at a higher level
                return default(T);
                //throw new InvalidDataException("The provided XML data is invalid.", ex);
            }
            catch (Exception ex)
            {
                // Log any other unexpected errors
                Log.Information("An unexpected error occurred during XML deserialization.", ex);
                // Re-throw the exception for higher-level handling
                return default(T);
                //throw new Exception("An unexpected error occurred during XML deserialization.", ex);
            }
        }

    }
}
