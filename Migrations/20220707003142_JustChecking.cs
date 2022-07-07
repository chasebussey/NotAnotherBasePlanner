using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotAnotherBasePlanner.Migrations
{
    public partial class JustChecking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Base_ApplicationUser_UserId1",
                table: "Base");

            migrationBuilder.DropIndex(
                name: "IX_Base_UserId1",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Base");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Base",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Base_ApplicationUserId",
                table: "Base",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_ApplicationUser_ApplicationUserId",
                table: "Base",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Base_ApplicationUser_ApplicationUserId",
                table: "Base");

            migrationBuilder.DropIndex(
                name: "IX_Base_ApplicationUserId",
                table: "Base");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Base");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Base",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Base",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Base_UserId1",
                table: "Base",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_ApplicationUser_UserId1",
                table: "Base",
                column: "UserId1",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }
    }
}
