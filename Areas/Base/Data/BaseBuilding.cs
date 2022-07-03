using System.ComponentModel.DataAnnotations;

namespace NotAnotherBasePlanner.Data;

public class BaseBuilding
{
    [Key]
    public Guid Id { get; set; }
    public Guid BaseId { get; set; }
    public Base Base { get; set; }
    public string BuildingTicker { get; set; }
    public Building Building { get; set; }
    public int? RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public double Efficiency { get; set; }
    public bool Constructed { get; set; }
}