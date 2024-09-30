using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels;
using RealEstate.Core.Models.ConcreteModels.Payments;
using RealEstate.Core.Models.ConcreteModels.Persons;

public class PaymentJsonConverter : JsonConverter<Payment>
{
    public override Payment Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var rootElement = jsonDocument.RootElement;

            if (rootElement.TryGetProperty("Type", out var typeProperty))
            {
                var typeString = typeProperty.GetString();
                switch (typeString)
                {
                    case "Bank":
                        return JsonSerializer.Deserialize<Bank>(rootElement.GetRawText(), options);
                    case "PayPal":
                        return JsonSerializer.Deserialize<PayPal>(rootElement.GetRawText(), options);
                    case "WesternUnion":
                        return JsonSerializer.Deserialize<WesternUnion>(rootElement.GetRawText(), options);
                    case "Vipps":
                        return JsonSerializer.Deserialize<Vipps>(rootElement.GetRawText(), options);
                    default:
                        throw new JsonException($"Unknown payment type: {typeString}");
                }
            }
            else
            {
                throw new JsonException("Unable to determine the person type during deserialization.");
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, Payment value, JsonSerializerOptions options)
    {
        var type = value.GetType();
        JsonSerializer.Serialize(writer, (object)value, type, options);
    }
}
