using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotAnotherBasePlanner.Migrations
{
    public partial class UpdateConsumablesLuxury : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Luxury",
                table: "Consumable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Luxury",
                table: "Consumable");
        }
    }
}
