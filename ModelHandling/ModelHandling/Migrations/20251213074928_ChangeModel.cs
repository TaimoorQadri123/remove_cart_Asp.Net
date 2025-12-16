using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelHandling.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Products_ProductsId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_ProductsId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Cart");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductId",
                table: "Cart",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Products_ProductId",
                table: "Cart",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Products_ProductId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_ProductId",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductsId",
                table: "Cart",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Products_ProductsId",
                table: "Cart",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
