using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KingCal.Data.Migrations
{
    public partial class ManyToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_SubCategory_SubCategoryId",
                schema: "Item",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_SubCategoryId",
                schema: "Item",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                schema: "Item",
                table: "Item");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                schema: "Item",
                table: "Item",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubItemId",
                schema: "Item",
                table: "Item",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SubCategoryItem",
                schema: "Item",
                columns: table => new
                {
                    SubCategoryId = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryItem", x => new { x.ItemId, x.SubCategoryId });
                    table.ForeignKey(
                        name: "FK_SubCategoryItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubCategoryItem_SubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalSchema: "Item",
                        principalTable: "SubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemId",
                schema: "Item",
                table: "Item",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryItem_SubCategoryId",
                schema: "Item",
                table: "SubCategoryItem",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Item_ItemId",
                schema: "Item",
                table: "Item",
                column: "ItemId",
                principalSchema: "Item",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Item_ItemId",
                schema: "Item",
                table: "Item");

            migrationBuilder.DropTable(
                name: "SubCategoryItem",
                schema: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemId",
                schema: "Item",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ItemId",
                schema: "Item",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SubItemId",
                schema: "Item",
                table: "Item");

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategoryId",
                schema: "Item",
                table: "Item",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Item_SubCategoryId",
                schema: "Item",
                table: "Item",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_SubCategory_SubCategoryId",
                schema: "Item",
                table: "Item",
                column: "SubCategoryId",
                principalSchema: "Item",
                principalTable: "SubCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
