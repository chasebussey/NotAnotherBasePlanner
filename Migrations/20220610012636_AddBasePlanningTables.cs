using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotAnotherBasePlanner.Migrations
{
    public partial class AddBasePlanningTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surface = table.Column<bool>(type: "bit", nullable: false),
                    Gravity = table.Column<double>(type: "float", nullable: false),
                    Pressure = table.Column<double>(type: "float", nullable: false),
                    Temp = table.Column<double>(type: "float", nullable: false),
                    Fertility = table.Column<double>(type: "float", nullable: false),
                    FactionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NearestCXCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaterialTicker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPrice_ApplicationUser_UserId1",
                        column: x => x.UserId1,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPrice_Material_MaterialTicker",
                        column: x => x.MaterialTicker,
                        principalTable: "Material",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Base",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AgricultureExperts = table.Column<int>(type: "int", nullable: false),
                    ChemistryExperts = table.Column<int>(type: "int", nullable: false),
                    ConstructionExperts = table.Column<int>(type: "int", nullable: false),
                    ElectronicsExperts = table.Column<int>(type: "int", nullable: false),
                    FoodExperts = table.Column<int>(type: "int", nullable: false),
                    FuelExperts = table.Column<int>(type: "int", nullable: false),
                    ManufacturingExperts = table.Column<int>(type: "int", nullable: false),
                    MetallurgyExperts = table.Column<int>(type: "int", nullable: false),
                    ExtractionExperts = table.Column<int>(type: "int", nullable: false),
                    Permits = table.Column<int>(type: "int", nullable: false),
                    AvailableArea = table.Column<int>(type: "int", nullable: false),
                    UsedArea = table.Column<int>(type: "int", nullable: false),
                    PlanetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Base_ApplicationUser_UserId1",
                        column: x => x.UserId1,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Base_Planet_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialTicker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Concentration = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_Material_MaterialTicker",
                        column: x => x.MaterialTicker,
                        principalTable: "Material",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resource_Planet_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BaseBuilding",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingTicker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: true),
                    Efficiency = table.Column<double>(type: "float", nullable: false),
                    Constructed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseBuilding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseBuilding_Base_BaseId",
                        column: x => x.BaseId,
                        principalTable: "Base",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseBuilding_Building_BuildingTicker",
                        column: x => x.BuildingTicker,
                        principalTable: "Building",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseBuilding_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Base_PlanetId",
                table: "Base",
                column: "PlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_Base_UserId1",
                table: "Base",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_BaseBuilding_BaseId",
                table: "BaseBuilding",
                column: "BaseId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseBuilding_BuildingTicker",
                table: "BaseBuilding",
                column: "BuildingTicker");

            migrationBuilder.CreateIndex(
                name: "IX_BaseBuilding_RecipeId",
                table: "BaseBuilding",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_MaterialTicker",
                table: "Resource",
                column: "MaterialTicker");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_PlanetId",
                table: "Resource",
                column: "PlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrice_MaterialTicker",
                table: "UserPrice",
                column: "MaterialTicker");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrice_UserId1",
                table: "UserPrice",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseBuilding");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "UserPrice");

            migrationBuilder.DropTable(
                name: "Base");

            migrationBuilder.DropTable(
                name: "Planet");
        }
    }
}
