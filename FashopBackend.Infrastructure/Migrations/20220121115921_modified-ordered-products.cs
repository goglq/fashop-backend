using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class modifiedorderedproducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordered_product_orders_order_id",
                table: "ordered_product");

            migrationBuilder.DropForeignKey(
                name: "FK_ordered_product_products_product_id",
                table: "ordered_product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ordered_product",
                table: "ordered_product");

            migrationBuilder.RenameTable(
                name: "ordered_product",
                newName: "ordered_products");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "ordered_products",
                newName: "count");

            migrationBuilder.RenameIndex(
                name: "IX_ordered_product_product_id",
                table: "ordered_products",
                newName: "IX_ordered_products_product_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ordered_products",
                table: "ordered_products",
                columns: new[] { "order_id", "product_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ordered_products_orders_order_id",
                table: "ordered_products",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ordered_products_products_product_id",
                table: "ordered_products",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordered_products_orders_order_id",
                table: "ordered_products");

            migrationBuilder.DropForeignKey(
                name: "FK_ordered_products_products_product_id",
                table: "ordered_products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ordered_products",
                table: "ordered_products");

            migrationBuilder.RenameTable(
                name: "ordered_products",
                newName: "ordered_product");

            migrationBuilder.RenameColumn(
                name: "count",
                table: "ordered_product",
                newName: "Count");

            migrationBuilder.RenameIndex(
                name: "IX_ordered_products_product_id",
                table: "ordered_product",
                newName: "IX_ordered_product_product_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ordered_product",
                table: "ordered_product",
                columns: new[] { "order_id", "product_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ordered_product_orders_order_id",
                table: "ordered_product",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ordered_product_products_product_id",
                table: "ordered_product",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
