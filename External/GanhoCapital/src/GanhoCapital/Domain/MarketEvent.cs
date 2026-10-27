using System.Text.Json.Serialization;

namespace GanhoCapital.Domain;

public class MarketEvent
{
    [JsonPropertyName("operation")]
    public OperationType Operation { get; set; }

    [JsonPropertyName("unit-cost")]
    public decimal UnitCost { get; set; }

    [JsonPropertyName("quantity")]
    public long Quantity { get; set; }
}