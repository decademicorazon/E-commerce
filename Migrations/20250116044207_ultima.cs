using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExperimentoAPI.Migrations
{
    /// <inheritdoc />
    public partial class ultima : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idCategoria",
                table: "productos2",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productos2_idCategoria",
                table: "productos2",
                column: "idCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_productos2_categorias_idCategoria",
                table: "productos2",
                column: "idCategoria",
                principalTable: "categorias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productos2_categorias_idCategoria",
                table: "productos2");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropIndex(
                name: "IX_productos2_idCategoria",
                table: "productos2");

            migrationBuilder.DropColumn(
                name: "idCategoria",
                table: "productos2");
        }
    }
}
