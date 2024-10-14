using RealEstateBLL.Models.BaseModels;
using RealEstateBLL.Models.ConcreteModels.Persons;
using System.Text.Json;
using System.Text.Json.Serialization;
//TODO add DTO since currently DAL needs refrence to BLL
namespace RealEstateDAL.JsonConverter
{

    public class PersonJsonConverter : JsonConverter<Person>
    {
        public override Person Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                var rootElement = jsonDocument.RootElement;

                if (rootElement.TryGetProperty("Type", out var typeProperty))
                {
                    var typeString = typeProperty.GetString();
                    switch (typeString)
                    {
                        case "Buyer":
                            return JsonSerializer.Deserialize<Buyer>(rootElement.GetRawText(), options);
                        case "Seller":
                            return JsonSerializer.Deserialize<Seller>(rootElement.GetRawText(), options);
                        default:
                            throw new JsonException($"Unknown person type: {typeString}");
                    }
                }
                else
                {
                    throw new JsonException("Unable to determine the person type during deserialization.");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, Person value, JsonSerializerOptions options)
        {
            var type = value.GetType();
            JsonSerializer.Serialize(writer, (object)value, type, options);
        }
    }
}