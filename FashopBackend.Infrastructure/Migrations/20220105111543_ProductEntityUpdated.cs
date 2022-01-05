using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class ProductEntityUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "products",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "products");

            migrationBuilder.DropColumn(
                name: "price",
                table: "products");
        }
    }
}
