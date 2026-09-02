using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CavipetrolTestBack.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class updatecolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Clientes",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "identificacion",
                table: "Clientes",
                newName: "Identificacion");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Clientes",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "apellido",
                table: "Clientes",
                newName: "Apellido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Clientes",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Identificacion",
                table: "Clientes",
                newName: "identificacion");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Clientes",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Apellido",
                table: "Clientes",
                newName: "apellido");
        }
    }
}
