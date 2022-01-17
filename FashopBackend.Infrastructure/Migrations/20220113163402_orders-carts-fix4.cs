using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class orderscartsfix4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_orders_OrderId",
                table: "cart");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "cart",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_cart_OrderId",
                table: "cart",
                newName: "IX_cart_order_id");

            migrationBuilder.AlterColumn<int>(
                name: "order_id",
                table: "cart",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_orders_order_id",
                table: "cart",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_orders_order_id",
                table: "cart");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "cart",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_cart_order_id",
                table: "cart",
                newName: "IX_cart_OrderId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "cart",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_orders_OrderId",
                table: "cart",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "id");
        }
    }
}
