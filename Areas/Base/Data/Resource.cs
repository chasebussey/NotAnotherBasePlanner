using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NotAnotherBasePlanner.Data;

public class Resource
{
	[Key] [JsonIgnore] public Guid Id { get; set; }

	[JsonIgnore] public string MaterialTicker { get; set; }

	[JsonIgnore] public Material Material { get; set; }

	[JsonPropertyName("Factor")] public double Concentration { get; set; }

	[JsonPropertyName("ResourceType")] public string Type { get; set; }

	[JsonPropertyName("MaterialId")] public string FIOId { get; set; }
}