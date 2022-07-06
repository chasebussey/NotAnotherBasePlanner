using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class PlannerContext : IdentityDbContext<ApplicationUser>
{
	public PlannerContext(DbContextOptions<PlannerContext> options) : base(options)
	{
	}

	public DbSet<Material> Materials { get; set; }
	public DbSet<Building> Buildings { get; set; }
	public DbSet<Recipe> Recipes { get; set; }
	public DbSet<Price> Prices { get; set; }
	public DbSet<MaterialRecipe> MaterialRecipes { get; set; }
	public DbSet<BuildingCost> BuildingCosts { get; set; }
	public DbSet<Base> Bases { get; set; }
	public DbSet<BaseBuilding> BaseBuildings { get; set; }
	public DbSet<Planet> Planets { get; set; }
	public DbSet<Resource> Resources { get; set; }
	public DbSet<UserPrice> UserPrices { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// remove some of the fields we don't need on the user
		modelBuilder.Entity<ApplicationUser>()
		            .Ignore(x => x.PhoneNumber)
		            .Ignore(x => x.PhoneNumberConfirmed);

		modelBuilder.Entity<Recipe>()
		            .Property(e => e.Inputs)
		            .HasConversion(
			            v => string.Join(',', v),
			            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
		            );

		modelBuilder.Entity<Recipe>()
		            .Property(e => e.Outputs)
		            .HasConversion(
			            v => string.Join(',', v),
			            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
		            );

		modelBuilder.Entity<Recipe>()
		            .Property(e => e.RecipeId).ValueGeneratedOnAdd();

		modelBuilder.Entity<Price>()
		            .HasKey(e => new { e.MaterialTicker, e.ExchangeCode });

		modelBuilder.Entity<MaterialRecipe>()
		            .HasKey(e => new { e.MaterialTicker, e.RecipeId });

		modelBuilder.Entity<BuildingCost>()
		            .HasKey(e => new { e.BuildingTicker, e.MaterialTicker });

		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			entityType.SetTableName(entityType.DisplayName());
	}
}