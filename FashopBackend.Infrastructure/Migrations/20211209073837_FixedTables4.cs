using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class FixedTables4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_products_categories",
                table: "products_categories");

            migrationBuilder.DropIndex(
                name: "IX_products_categories_category_id",
                table: "products_categories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "products_categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "products_categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products_categories",
                table: "products_categories",
                columns: new[] { "category_id", "product_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_products_categories",
                table: "products_categories");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "products_categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "products_categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_products_categories",
                table: "products_categories",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_products_categories_category_id",
                table: "products_categories",
                column: "category_id");
        }
    }
}
