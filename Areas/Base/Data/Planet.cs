using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace NotAnotherBasePlanner.Data;

public class Planet
{
    [Key]
    public Guid Id { get; set; }
    [JsonPropertyName("PlanetNaturalId")]
    public string Designation { get; set; }
    [JsonPropertyName("PlanetName")]
    public string? DisplayName { get; set; }
    public List<Resource>? Resources { get; set; }
    public bool Surface { get; set; }
    public double Gravity { get; set; }
    public double Pressure { get; set; }
    public double Temp { get; set; }
    public double Fertility { get; set; }
    public string? FactionCode { get; set; }
    public string NearestCXCode { get; set; }
}