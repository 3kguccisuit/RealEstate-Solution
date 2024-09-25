using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels;

public class EstateJsonConverter : JsonConverter<Estate>
{
    public override Estate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;

            // Check the "Type" property to determine whether it's a Villa or Apartment
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
