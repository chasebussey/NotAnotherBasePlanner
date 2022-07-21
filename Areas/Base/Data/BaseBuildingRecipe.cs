using System.ComponentModel.DataAnnotations.Schema;

namespace NotAnotherBasePlanner.Data;

public class BaseBuildingRecipe
{
	public Guid Id { get; set; }
	public Guid BaseBuildingId { get; set; }
	public BaseBuilding Building { get; set; }
	public int? RecipeId { get; set; }
	public Recipe? Recipe { get; set; }
	
	public double Allocation { get; set; }
}