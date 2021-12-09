using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class FixedTables3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_categories_CategoryId",
                table: "products_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_products_ProductId",
                table: "products_categories");

            migrationBuilder.DropIndex(
                name: "IX_products_categories_CategoryId",
                table: "products_categories");

            migrationBuilder.AddColumn<int>(
                name: "category_id",
                table: "products_categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "product_id",
                table: "products_categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_categories_category_id",
                table: "products_categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_categories_product_id",
                table: "products_categories",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_categories_category_id",
                table: "products_categories",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_products_product_id",
                table: "products_categories",
                column: "product_id",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_categories_category_id",
                table: "products_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_products_product_id",
                table: "products_categories");

            migrationBuilder.DropIndex(
                name: "IX_products_categories_category_id",
                table: "products_categories");

            migrationBuilder.DropIndex(
                name: "IX_products_categories_product_id",
                table: "products_categories");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "products_categories");

            migrationBuilder.DropColumn(
                name: "product_id",
                table: "products_categories");

            migrationBuilder.CreateIndex(
                name: "IX_products_categories_CategoryId",
                table: "products_categories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_categories_CategoryId",
                table: "products_categories",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_products_ProductId",
                table: "products_categories",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
