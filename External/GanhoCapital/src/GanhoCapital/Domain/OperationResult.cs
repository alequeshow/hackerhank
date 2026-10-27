using System.Text.Json.Serialization;

namespace GanhoCapital.Domain;

public class OperationResult
{
    [JsonPropertyName("tax")]    
    public decimal? Tax { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }
}