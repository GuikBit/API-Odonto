using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistaApi.Migrations
{
    /// <inheritdoc />
    public partial class Cargov2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Departamento",
                table: "Cargos",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Departamento",
                table: "Cargos");
        }
    }
}
