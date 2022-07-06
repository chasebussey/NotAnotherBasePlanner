using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class MaterialRecipeService
{
	public MaterialRecipeService(PlannerContext dbContext, MaterialService materialService, RecipeService recipeService)
	{
		DbContext       = dbContext;
		MaterialService = materialService;
		RecipeService   = recipeService;
	}

	private PlannerContext DbContext { get; }
	private MaterialService MaterialService { get; }
	private RecipeService RecipeService { get; }

	public void UpdateAllMaterialRecipes()
	{
		// TODO: This throws an error, don't know why, but it seems to work
		foreach (var recipe in DbContext.Recipes)
		{
			foreach (var materialTicker in recipe.Outputs)
			{
				var materialRecipe = new MaterialRecipe
				{
					MaterialTicker = materialTicker,
					RecipeId       = recipe.RecipeId
				};

				DbContext.MaterialRecipes.Add(materialRecipe);
			}
		}

		DbContext.SaveChanges();
	}

	public MaterialRecipe[] GetMaterialRecipes()
	{
		return DbContext.MaterialRecipes.ToArray();
	}

	public async Task<MaterialRecipe[]> GetMaterialRecipesAsync()
	{
		return await DbContext.MaterialRecipes.ToArrayAsync();
	}

	public async Task<MaterialRecipe[]> GetMaterialRecipesByTicker(string materialTicker)
	{
		return await DbContext.MaterialRecipes.Where(x => x.MaterialTicker == materialTicker).ToArrayAsync();
	}
}