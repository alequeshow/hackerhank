using System.Text.Json;
using System.Text.Json.Serialization;

namespace GanhoCapital.JsonHelpers;

public class DecimalJsonConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDecimal();
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        // Always format decimal with exactly one decimal place, forcing display of .0
        var roundedValue = Math.Round(value, 1);
        
        // Use raw value writing to force one decimal place
        writer.WriteRawValue(roundedValue.ToString("0.0"));
    }
}