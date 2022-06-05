using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotAnotherBasePlanner.Migrations
{
    public partial class AddCorpTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorporationCode",
                table: "ApplicationUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Corporation",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporation", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "CorporateProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorpCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorporateProject_ApplicationUser_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CorporateProject_Corporation_CorpCode",
                        column: x => x.CorpCode,
                        principalTable: "Corporation",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorporateProjectItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialTicker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CorporateProjectId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorporateProjectId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateProjectItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorporateProjectItem_ApplicationUser_UserID",
                        column: x => x.UserID,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorporateProjectItem_CorporateProject_CorporateProjectId1",
                        column: x => x.CorporateProjectId1,
                        principalTable: "CorporateProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorporateProjectItem_Material_MaterialTicker",
                        column: x => x.MaterialTicker,
                        principalTable: "Material",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorporateProjectItem_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorporateProjectUser",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CorporateProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CorporateProjectId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateProjectUser", x => new { x.UserId, x.CorporateProjectId });
                    table.ForeignKey(
                        name: "FK_CorporateProjectUser_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorporateProjectUser_CorporateProject_CorporateProjectId1",
                        column: x => x.CorporateProjectId1,
                        principalTable: "CorporateProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_CorporationCode",
                table: "ApplicationUser",
                column: "CorporationCode");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateProject_CorpCode",
                table: "CorporateProject",
                column: "CorpCode");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateProject_OwnerId",
                table: "CorporateProject",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateProjectItem_CorporateProjectId1",
                table: "CorporateProjectItem",
                column: "CorporateProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateProjectItem_MaterialTicker",
                table: "CorporateProjectItem",
                column: "MaterialTicker");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateProjectItem_RecipeId",
                table: "CorporateProjectItem",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateProjectItem_UserID",
                table: "CorporateProjectItem",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateProjectUser_CorporateProjectId1",
                table: "CorporateProjectUser",
                column: "CorporateProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Corporation_CorporationCode",
                table: "ApplicationUser",
                column: "CorporationCode",
                principalTable: "Corporation",
                principalColumn: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Corporation_CorporationCode",
                table: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "CorporateProjectItem");

            migrationBuilder.DropTable(
                name: "CorporateProjectUser");

            migrationBuilder.DropTable(
                name: "CorporateProject");

            migrationBuilder.DropTable(
                name: "Corporation");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_CorporationCode",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "CorporationCode",
                table: "ApplicationUser");
        }
    }
}
