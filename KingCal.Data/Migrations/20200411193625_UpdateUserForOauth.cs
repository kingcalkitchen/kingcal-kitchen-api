using Microsoft.EntityFrameworkCore.Migrations;

namespace KingCal.Data.Migrations
{
    public partial class UpdateUserForOauth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "User",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OauthIssuer",
                schema: "User",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OauthSubject",
                schema: "User",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "User",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OauthIssuer",
                schema: "User",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OauthSubject",
                schema: "User",
                table: "Users");
        }
    }
}
