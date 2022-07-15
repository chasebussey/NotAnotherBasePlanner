using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class RecipeService
{
	public RecipeService(PlannerContext dbContext)
	{
		DbContext = dbContext;
	}

	private PlannerContext DbContext { get; }

	public async void UpdateAllRecipesFromFIO()
	{
		var recipes = new List<Recipe>();

		var fioClient = new HttpClient();
		var response  = await fioClient.GetAsync("https://rest.fnar.net/recipes/allrecipes");

		if (response.IsSuccessStatusCode)
		{
			using var contentStream = await response.Content.ReadAsStreamAsync();
			recipes = await JsonSerializer.DeserializeAsync<List<Recipe>>(contentStream);
		}

		foreach (var recipe in recipes) DbContext.Recipes.Add(recipe);

		DbContext.SaveChanges();
	}

	public async Task<Recipe[]> GetRecipesAsync()
	{
		return await DbContext.Recipes.ToArrayAsync();
	}

	public Recipe[] GetRecipes()
	{
		return DbContext.Recipes.ToArray();
	}

	public async Task<Recipe[]> GetRecipesByOutputAsync(string outputTicker)
	{
		return await DbContext.Recipes.Where(x => x.Outputs.Contains(outputTicker)).ToArrayAsync();
	}

	public async Task<Recipe[]> GetRecipesByInputAsync(string inputTicker)
	{
		return await DbContext.Recipes.Where(x => x.Inputs.Contains(inputTicker)).ToArrayAsync();
	}

	public async Task<Recipe[]> GetRecipesByBuildingAsync(string buildingTicker)
	{
		return await DbContext.Recipes.Where(x => 
			                                     x.BuildingTicker.Equals(buildingTicker, StringComparison.InvariantCultureIgnoreCase))
		                      .ToArrayAsync();
	}
}