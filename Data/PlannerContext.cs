using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NotAnotherBasePlanner.Data;

public class PlannerContext : IdentityDbContext
{
    public DbSet<Material> Materials { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<MaterialRecipe> MaterialRecipes { get; set; }
    public DbSet<BuildingCost> BuildingCosts { get; set; }

    public PlannerContext(DbContextOptions<PlannerContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            entityType.SetTableName(entityType.DisplayName());
        }
    }
}