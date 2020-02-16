using Microsoft.EntityFrameworkCore.Migrations;

namespace KingCal.Data.Migrations
{
    public partial class AddAddressTypeToAddressClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressType",
                schema: "Address",
                table: "Address",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressType",
                schema: "Address",
                table: "Address");
        }
    }
}
