using System.ComponentModel.DataAnnotations;

namespace NotAnotherBasePlanner.Data;

public class Resource
{
    [Key]
    public Guid Id { get; set; }
    public string MaterialTicker { get; set; }
    public Material Material { get; set; }

    public double Concentration { get; set; }
    public string Type { get; set; }
}