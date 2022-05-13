using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NotAnotherBasePlanner.Data;

public class PlannerContext : DbContext
{
    public DbSet<Material> Materials { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

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

        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            entityType.SetTableName(entityType.DisplayName());
        }
    }
}