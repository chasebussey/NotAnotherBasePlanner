using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotAnotherBasePlanner.Migrations
{
    public partial class ModifyIdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "ApplicationUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "ApplicationUser",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
