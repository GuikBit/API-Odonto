using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistaApi.Migrations
{
    /// <inheritdoc />
    public partial class ConsultaAjuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcedimentoConsulta",
                table: "Consultas",
                newName: "Procedimentos");

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Consultas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Consultas");

            migrationBuilder.RenameColumn(
                name: "Procedimentos",
                table: "Consultas",
                newName: "ProcedimentoConsulta");
        }
    }
}
