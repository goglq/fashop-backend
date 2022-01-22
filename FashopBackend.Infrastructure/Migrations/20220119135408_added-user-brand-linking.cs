using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class addeduserbrandlinking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "brands",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_brands_user_id",
                table: "brands",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_brands_users_user_id",
                table: "brands",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_brands_users_user_id",
                table: "brands");

            migrationBuilder.DropIndex(
                name: "IX_brands_user_id",
                table: "brands");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "brands");
        }
    }
}
