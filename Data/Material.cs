using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NotAnotherBasePlanner.Data;

public class Material
{
    public string? CategoryName { get; set; }
    public string Name { get; set; }

    [Key]
    public string Ticker { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }
    [JsonPropertyName("MaterialId")]
    public string FIOId { get; set; }

    public Material(string categoryName, string name, string ticker, double weight, double volume)
    {
        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(ticker))
        {
            throw new ArgumentException("Material must include a name and ticker.");
        }

        this.CategoryName = categoryName;
        this.Name = name;
        this.Ticker = ticker;
        this.Weight = weight;
        this.Volume = volume;
    }
}