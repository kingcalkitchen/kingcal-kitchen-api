using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KingCal.Data.Migrations
{
    public partial class initial_migration_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Address");

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    AdministrativeArea = table.Column<string>(nullable: true),
                    SubAdministrativeArea = table.Column<string>(nullable: true),
                    Locality = table.Column<string>(nullable: true),
                    PostalCode = table.Column<int>(nullable: false),
                    StreetAddress = table.Column<string>(nullable: true),
                    Premise = table.Column<string>(nullable: true),
                    LandMark = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "Address");
        }
    }
}
