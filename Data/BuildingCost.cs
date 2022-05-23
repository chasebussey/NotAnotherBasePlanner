using System.Text.Json.Serialization;

namespace NotAnotherBasePlanner.Data;

public class BuildingCost
{
    public string BuildingTicker { get; set; }
    public Building Building { get; set; }

    [JsonPropertyName("CommodityTicker")]
    public string MaterialTicker { get; set; }
    public Material Material { get; set; }
    public int Amount { get; set; }
}