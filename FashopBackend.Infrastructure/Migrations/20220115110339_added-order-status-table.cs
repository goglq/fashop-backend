using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class addedorderstatustable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "status_id",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "order_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_statuses", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "order_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 0, "Confirming" },
                    { 1, "Packing" },
                    { 2, "Delivering" },
                    { 3, "Delivered" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_status_id",
                table: "orders",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_order_statuses_status_id",
                table: "orders",
                column: "status_id",
                principalTable: "order_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_order_statuses_status_id",
                table: "orders");

            migrationBuilder.DropTable(
                name: "order_statuses");

            migrationBuilder.DropIndex(
                name: "IX_orders_status_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "orders",
                type: "text",
                nullable: true);
        }
    }
}
