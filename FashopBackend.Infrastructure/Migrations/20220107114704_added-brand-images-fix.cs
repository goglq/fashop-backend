using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class addedbrandimagesfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "brand_image_id",
                table: "brands",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "brand_images",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    thumbnail = table.Column<string>(type: "text", nullable: true),
                    header = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brand_images", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_brands_brand_image_id",
                table: "brands",
                column: "brand_image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_brands_brand_images_brand_image_id",
                table: "brands",
                column: "brand_image_id",
                principalTable: "brand_images",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_brands_brand_images_brand_image_id",
                table: "brands");

            migrationBuilder.DropTable(
                name: "brand_images");

            migrationBuilder.DropIndex(
                name: "IX_brands_brand_image_id",
                table: "brands");

            migrationBuilder.DropColumn(
                name: "brand_image_id",
                table: "brands");
        }
    }
}
