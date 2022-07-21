using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotAnotherBasePlanner.Migrations
{
    public partial class QuantityAndAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Allocation",
                table: "BaseBuildingRecipe",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BaseBuilding",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allocation",
                table: "BaseBuildingRecipe");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BaseBuilding");
        }
    }
}
