using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseId1",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PurchaseId1",
                table: "Products",
                column: "PurchaseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Purchase_PurchaseId1",
                table: "Products",
                column: "PurchaseId1",
                principalTable: "Purchase",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Purchase_PurchaseId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PurchaseId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchaseId1",
                table: "Products");
        }
    }
}
