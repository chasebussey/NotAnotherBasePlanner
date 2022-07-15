using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotAnotherBasePlanner.Migrations
{
    public partial class BaseBuildingRecipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseBuilding_Recipe_RecipeId",
                table: "BaseBuilding");

            migrationBuilder.DropIndex(
                name: "IX_BaseBuilding_RecipeId",
                table: "BaseBuilding");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "BaseBuilding");

            migrationBuilder.CreateTable(
                name: "BaseBuildingRecipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseBuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseBuildingRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseBuildingRecipe_BaseBuilding_BaseBuildingId",
                        column: x => x.BaseBuildingId,
                        principalTable: "BaseBuilding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseBuildingRecipe_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseBuildingRecipe_BaseBuildingId",
                table: "BaseBuildingRecipe",
                column: "BaseBuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseBuildingRecipe_RecipeId",
                table: "BaseBuildingRecipe",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseBuildingRecipe");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "BaseBuilding",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseBuilding_RecipeId",
                table: "BaseBuilding",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseBuilding_Recipe_RecipeId",
                table: "BaseBuilding",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "RecipeId");
        }
    }
}
