using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class FixedTables1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_categories_ProductsId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_products_ProductsId1",
                table: "CategoryProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryProduct",
                table: "CategoryProduct");

            migrationBuilder.DropIndex(
                name: "IX_CategoryProduct_ProductsId1",
                table: "CategoryProduct");

            migrationBuilder.RenameTable(
                name: "CategoryProduct",
                newName: "products_categories");

            migrationBuilder.RenameColumn(
                name: "ProductsId1",
                table: "products_categories",
                newName: "CategoriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products_categories",
                table: "products_categories",
                columns: new[] { "CategoriesId", "ProductsId" });

            migrationBuilder.CreateIndex(
                name: "IX_products_categories_ProductsId",
                table: "products_categories",
                column: "ProductsId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_categories_CategoriesId",
                table: "products_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_products_ProductsId",
                table: "products_categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products_categories",
                table: "products_categories");

            migrationBuilder.DropIndex(
                name: "IX_products_categories_ProductsId",
                table: "products_categories");

            migrationBuilder.RenameTable(
                name: "products_categories",
                newName: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "CategoryProduct",
                newName: "ProductsId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryProduct",
                table: "CategoryProduct",
                columns: new[] { "ProductsId", "ProductsId1" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId1",
                table: "CategoryProduct",
                column: "ProductsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_categories_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_products_ProductsId1",
                table: "CategoryProduct",
                column: "ProductsId1",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
