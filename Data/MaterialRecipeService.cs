using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class MaterialRecipeService
{
    private PlannerContext DbContext { get; set; }
    private MaterialService MaterialService { get; set; }
    private RecipeService RecipeService { get; set; }

    public MaterialRecipeService(PlannerContext dbContext, MaterialService materialService, RecipeService recipeService)
    {
        this.DbContext = dbContext;
        this.MaterialService = materialService;
        this.RecipeService = recipeService;
    }

    public void UpdateAllMaterialRecipes()
    {
        // TODO: This throws an error, don't know why, but it seems to work
        foreach (Recipe recipe in DbContext.Recipes)
        {
            foreach (string materialTicker in recipe.Outputs)
            {
                MaterialRecipe materialRecipe = new MaterialRecipe
                {
                    MaterialTicker = materialTicker,
                    RecipeId = recipe.RecipeId
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