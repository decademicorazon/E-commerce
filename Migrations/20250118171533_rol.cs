using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExperimentoAPI.Migrations
{
    /// <inheritdoc />
    public partial class rol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idRol",
                table: "Consumidor",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "nombre" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "usuario" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumidor_idRol",
                table: "Consumidor",
                column: "idRol");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumidor_roles_idRol",
                table: "Consumidor",
                column: "idRol",
                principalTable: "roles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumidor_roles_idRol",
                table: "Consumidor");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropIndex(
                name: "IX_Consumidor_idRol",
                table: "Consumidor");

            migrationBuilder.DropColumn(
                name: "idRol",
                table: "Consumidor");
        }
    }
}
