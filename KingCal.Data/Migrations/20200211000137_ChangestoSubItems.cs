using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KingCal.Data.Migrations
{
    public partial class ChangestoSubItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_SubItem_SubItemId",
                schema: "Item",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_SubItemId",
                schema: "Item",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "SubItemId",
                schema: "Item",
                table: "Property");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubItemId",
                schema: "Item",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Property_SubItemId",
                schema: "Item",
                table: "Property",
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
    }
}
