using DTO.Models.BaseModels;
using DTO.Models.ConcreteModels;
using System.Text.Json;
using System.Text.Json.Serialization;
    //TODO add DTO since currently DAL needs refrence to BLL
namespace RealEstateDAL.JsonConverter
{
public class EstateJsonConverter : JsonConverter<Estate>
{
    public override Estate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;

            if (rootElement.TryGetProperty("Type", out var typeProperty))
            {
                var typeString = typeProperty.GetString();
                switch (typeString)
                {
                    case "Villa":
                        return JsonSerializer.Deserialize<Villa>(rootElement.GetRawText(), options);
                    case "Apartment":
                        return JsonSerializer.Deserialize<Apartment>(rootElement.GetRawText(), options);
                    case "Townhouse":
                        return JsonSerializer.Deserialize<Townhouse>(rootElement.GetRawText(), options);
                    case "Hospital":
                        return JsonSerializer.Deserialize<Hospital>(rootElement.GetRawText(), options);
                    case "School":
                        return JsonSerializer.Deserialize<School>(rootElement.GetRawText(), options);
                    case "University":
                        return JsonSerializer.Deserialize<University>(rootElement.GetRawText(), options);
                    case "Hotel":
                        return JsonSerializer.Deserialize<Hotel>(rootElement.GetRawText(), options);
                    case "Shop":
                        return JsonSerializer.Deserialize<Shop>(rootElement.GetRawText(), options);
                    case "Warehouse":
                        return JsonSerializer.Deserialize<Warehouse>(rootElement.GetRawText(), options);
                    case "Factory":
                        return JsonSerializer.Deserialize<Factory>(rootElement.GetRawText(), options);
                    default:
                        throw new JsonException($"Unknown estate type: {typeString}");
                }
            }
            else
            {
                throw new JsonException("Unable to determine the estate type during deserialization.");
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, Estate value, JsonSerializerOptions options)
    {
        var type = value.GetType();
        JsonSerializer.Serialize(writer, (object)value, type, options);
    }
}
}