using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class addedtotalpriceinorderandchangedcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_orders_order_id",
                table: "carts");

            migrationBuilder.DropIndex(
                name: "IX_carts_order_id",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "order_id",
                table: "carts");

            migrationBuilder.AddColumn<double>(
                name: "total_price",
                table: "orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_price",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "order_id",
                table: "carts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_carts_order_id",
                table: "carts",
                column: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_orders_order_id",
                table: "carts",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id");
        }
    }
}
