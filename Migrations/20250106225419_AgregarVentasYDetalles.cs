using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExperimentoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarVentasYDetalles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ventas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuarioId = table.Column<int>(type: "integer", nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ventas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "detallesVenta",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ventaId = table.Column<int>(type: "integer", nullable: false),
                    idProducto = table.Column<int>(type: "integer", nullable: false),
                    nombreProducto = table.Column<string>(type: "text", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    precioUnitario = table.Column<int>(type: "integer", nullable: false),
                    subTotal = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detallesVenta", x => x.id);
                    table.ForeignKey(
                        name: "FK_detallesVenta_ventas_ventaId",
                        column: x => x.ventaId,
                        principalTable: "ventas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_detallesVenta_ventaId",
                table: "detallesVenta",
                column: "ventaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detallesVenta");

            migrationBuilder.DropTable(
                name: "ventas");
        }
    }
}
