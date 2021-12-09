using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class FixedTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_categories_CategoriesId",
                table: "products_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_products_ProductsId",
                table: "products_categories");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "products_categories",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "products_categories",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_products_categories_ProductsId",
                table: "products_categories",
                newName: "IX_products_categories_CategoryId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_categories_CategoryId",
                table: "products_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_products_ProductId",
                table: "products_categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "products_categories",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "products_categories",
                newName: "CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_products_categories_CategoryId",
                table: "products_categories",
                newName: "IX_products_categories_ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_categories_CategoriesId",
                table: "products_categories",
                column: "CategoriesId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_products_ProductsId",
                table: "products_categories",
                column: "ProductsId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
