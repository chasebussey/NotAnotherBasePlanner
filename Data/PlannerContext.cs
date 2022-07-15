using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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

	public DbSet<BaseBuildingRecipe> BaseBuildingRecipes { get; set; }

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

		modelBuilder.Entity<Base>()
		            .Navigation(e => e.Planet)
		            .AutoInclude();
		modelBuilder.Entity<Base>()
		            .Navigation(e => e.Buildings)
		            .AutoInclude();
		modelBuilder.Entity<BaseBuilding>()
		            .Navigation(e => e.Building)
		            .AutoInclude();

		modelBuilder.Entity<BaseBuilding>()
		            .HasMany(e => e.Recipes)
		            .WithOne(e => e.Building)
		            .HasForeignKey(e => e.BaseBuildingId);
		
		
		// modelBuilder.Entity<BaseBuildingRecipe>().HasOne(e => e.Building).WithOne().OnDelete(DeleteBehavior.NoAction);
		// modelBuilder.Entity<BaseBuildingRecipe>().HasOne(e => e.Recipe).WithOne().OnDelete(DeleteBehavior.NoAction);

		modelBuilder.Entity<Building>()
		            .Navigation(e => e.Recipes)
		            .AutoInclude();

		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			entityType.SetTableName(entityType.DisplayName());
	}
}