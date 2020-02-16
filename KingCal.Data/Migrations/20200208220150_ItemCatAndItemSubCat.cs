using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KingCal.Data.Migrations
{
    public partial class ItemCatAndItemSubCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Item");

            migrationBuilder.CreateTable(
                name: "ItemCategory",
                schema: "Item",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemSubCategory",
                schema: "Item",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ItemCategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSubCategory_ItemCategory_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalSchema: "Item",
                        principalTable: "ItemCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemSubCategory_ItemCategoryId",
                schema: "Item",
                table: "ItemSubCategory",
                column: "ItemCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemSubCategory",
                schema: "Item");

            migrationBuilder.DropTable(
                name: "ItemCategory",
                schema: "Item");
        }
    }
}
