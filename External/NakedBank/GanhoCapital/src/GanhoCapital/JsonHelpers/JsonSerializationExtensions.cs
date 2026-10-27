using System.Text.Json;

namespace GanhoCapital.JsonHelpers;

public static class JsonSerializationExtensions
{
    /// <summary>
    /// Serializes an object to JSON with custom formatting that includes:
    /// - Camel case property naming
    /// - Decimal values with one decimal place
    /// - Proper spacing around colons for readability
    /// </summary>
    /// <typeparam name="T">The type of object to serialize</typeparam>
    /// <param name="obj">The object to serialize</param>
    /// <returns>A formatted JSON string with proper spacing and decimal formatting</returns>
    public static string ToInlineJson<T>(this T obj)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new DecimalJsonConverter() }
        };
        
        // System.Text.Json doesn't have built-in spacing control, so we use targeted replacement
        var jsonString = JsonSerializer.Serialize(obj, jsonOptions);
        var result = jsonString.Replace("\":\"", "\": \"").Replace("\":", "\": ");
        
        return result;
    }
}