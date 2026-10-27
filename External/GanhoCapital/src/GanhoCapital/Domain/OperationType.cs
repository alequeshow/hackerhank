using System.Text.Json.Serialization;

namespace GanhoCapital.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OperationType
{
    Buy,
    Sell
}