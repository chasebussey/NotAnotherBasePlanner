using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotAnotherBasePlanner.Migrations
{
    public partial class CreateBuildingCostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildingCost",
                columns: table => new
                {
                    BuildingTicker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialTicker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingCost", x => new { x.BuildingTicker, x.MaterialTicker });
                    table.ForeignKey(
                        name: "FK_BuildingCost_Building_BuildingTicker",
                        column: x => x.BuildingTicker,
                        principalTable: "Building",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingCost_Material_MaterialTicker",
                        column: x => x.MaterialTicker,
                        principalTable: "Material",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildingCost_MaterialTicker",
                table: "BuildingCost",
                column: "MaterialTicker");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingCost");
        }
    }
}
