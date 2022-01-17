using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class fixedtableandcolumnnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_orders_order_id",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_products_product_id",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_users_user_id",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_roleId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cart",
                table: "cart");

            migrationBuilder.RenameTable(
                name: "cart",
                newName: "carts");

            migrationBuilder.RenameColumn(
                name: "roleId",
                table: "users",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_roleId",
                table: "users",
                newName: "IX_users_role_id");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "carts",
                newName: "count");

            migrationBuilder.RenameIndex(
                name: "IX_cart_user_id",
                table: "carts",
                newName: "IX_carts_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_cart_product_id",
                table: "carts",
                newName: "IX_carts_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_cart_order_id",
                table: "carts",
                newName: "IX_carts_order_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_carts",
                table: "carts",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_orders_order_id",
                table: "carts",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_products_product_id",
                table: "carts",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carts_users_user_id",
                table: "carts",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_orders_order_id",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_carts_products_product_id",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_carts_users_user_id",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_role_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_carts",
                table: "carts");

            migrationBuilder.RenameTable(
                name: "carts",
                newName: "cart");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "users",
                newName: "roleId");

            migrationBuilder.RenameIndex(
                name: "IX_users_role_id",
                table: "users",
                newName: "IX_users_roleId");

            migrationBuilder.RenameColumn(
                name: "count",
                table: "cart",
                newName: "Count");

            migrationBuilder.RenameIndex(
                name: "IX_carts_user_id",
                table: "cart",
                newName: "IX_cart_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_carts_product_id",
                table: "cart",
                newName: "IX_cart_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_carts_order_id",
                table: "cart",
                newName: "IX_cart_order_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart",
                table: "cart",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_orders_order_id",
                table: "cart",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_products_product_id",
                table: "cart",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_users_user_id",
                table: "cart",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_roleId",
                table: "users",
                column: "roleId",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
