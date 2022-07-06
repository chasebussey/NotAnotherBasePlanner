namespace NotAnotherBasePlanner.Data;

public class MaterialRecipe
{
	public string MaterialTicker { get; set; }
	public Material Material { get; set; }
	public int RecipeId { get; set; }
	public Recipe Recipe { get; set; }
}