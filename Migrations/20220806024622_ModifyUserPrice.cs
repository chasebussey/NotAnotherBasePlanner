using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotAnotherBasePlanner.Migrations
{
    public partial class ModifyUserPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPrice_ApplicationUser_UserId1",
                table: "UserPrice");

            migrationBuilder.DropIndex(
                name: "IX_UserPrice_UserId1",
                table: "UserPrice");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserPrice");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserPrice",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "UserPrice",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrice_UserId",
                table: "UserPrice",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrice_ApplicationUser_UserId",
                table: "UserPrice",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPrice_ApplicationUser_UserId",
                table: "UserPrice");

            migrationBuilder.DropIndex(
                name: "IX_UserPrice_UserId",
                table: "UserPrice");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserPrice",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "UserPrice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserPrice",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPrice_UserId1",
                table: "UserPrice",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrice_ApplicationUser_UserId1",
                table: "UserPrice",
                column: "UserId1",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }
    }
}
