using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExperimentoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Hola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "precioUnitario",
                table: "detallesVenta",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "precioUnitario",
                table: "detallesVenta",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
