using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    public partial class addedcommercialtableandcommercialtypesenumtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "commercial_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commercial_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "commercials",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    url = table.Column<string>(type: "text", nullable: true),
                    commercial_type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commercials", x => x.id);
                    table.ForeignKey(
                        name: "FK_commercials_commercial_types_commercial_type_id",
                        column: x => x.commercial_type_id,
                        principalTable: "commercial_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "commercial_types",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 0, "Grand" },
                    { 1, "Banner" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_commercials_commercial_type_id",
                table: "commercials",
                column: "commercial_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "commercials");

            migrationBuilder.DropTable(
                name: "commercial_types");
        }
    }
}
