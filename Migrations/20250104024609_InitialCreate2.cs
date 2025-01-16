using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExperimentoAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoDetalles_productos_ProductoId",
                table: "CarritoDetalles");

            migrationBuilder.CreateTable(
                name: "productos2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos2", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoDetalles_productos2_ProductoId",
                table: "CarritoDetalles",
                column: "ProductoId",
                principalTable: "productos2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoDetalles_productos2_ProductoId",
                table: "CarritoDetalles");

            migrationBuilder.DropTable(
                name: "productos2");

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoDetalles_productos_ProductoId",
                table: "CarritoDetalles",
                column: "ProductoId",
                principalTable: "productos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
