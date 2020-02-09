using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KingCal.Data.Migrations
{
    public partial class AddIteSubItemtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Item_ItemId",
                schema: "Item",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemId",
                schema: "Item",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ItemId",
                schema: "Item",
                table: "Item");

            migrationBuilder.AddColumn<Guid>(
                name: "SubItemId",
                schema: "Item",
                table: "Property",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubItem",
                schema: "Item",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemSubItem",
                schema: "Item",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(nullable: false),
                    SubItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSubItem", x => new { x.ItemId, x.SubItemId });
                    table.ForeignKey(
                        name: "FK_ItemSubItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSubItem_SubItem_SubItemId",
                        column: x => x.SubItemId,
                        principalSchema: "Item",
                        principalTable: "SubItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Property_SubItemId",
                schema: "Item",
                table: "Property",
                column: "SubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSubItem_SubItemId",
                schema: "Item",
                table: "ItemSubItem",
                column: "SubItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_SubItem_SubItemId",
                schema: "Item",
                table: "Property",
                column: "SubItemId",
                principalSchema: "Item",
                principalTable: "SubItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_SubItem_SubItemId",
                schema: "Item",
                table: "Property");

            migrationBuilder.DropTable(
                name: "ItemSubItem",
                schema: "Item");

            migrationBuilder.DropTable(
                name: "SubItem",
                schema: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Property_SubItemId",
                schema: "Item",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "SubItemId",
                schema: "Item",
                table: "Property");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                schema: "Item",
                table: "Item",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemId",
                schema: "Item",
                table: "Item",
                column: "ItemId");

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
    }
}
