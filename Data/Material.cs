using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NotAnotherBasePlanner.Data;

public class Material
{
	public Material(string categoryName, string name, string ticker, double weight, double volume)
	{
		if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(ticker))
			throw new ArgumentException("Material must include a name and ticker.");

		CategoryName = categoryName;
		Name         = name;
		Ticker       = ticker;
		Weight       = weight;
		Volume       = volume;
	}

	public string? CategoryName { get; set; }
	public string Name { get; set; }

	[Key] public string Ticker { get; set; }

	public double Weight { get; set; }
	public double Volume { get; set; }

	[JsonPropertyName("MaterialId")] public string FIOId { get; set; }
}