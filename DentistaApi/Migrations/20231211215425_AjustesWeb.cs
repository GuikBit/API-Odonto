using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistaApi.Migrations
{
    /// <inheritdoc />
    public partial class AjustesWeb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dataNasc",
                table: "Pacientes",
                newName: "DataNascimento");

            migrationBuilder.RenameColumn(
                name: "Rua",
                table: "Enderecos",
                newName: "Logradouro");

            migrationBuilder.RenameColumn(
                name: "dataNasc",
                table: "Dentistas",
                newName: "DataNascimento");

            migrationBuilder.RenameColumn(
                name: "dataNasc",
                table: "Administrador",
                newName: "DataNascimento");

            migrationBuilder.AlterColumn<int>(
                name: "PacienteId",
                table: "Responsavel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumPasta",
                table: "Pacientes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumPasta",
                table: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "DataNascimento",
                table: "Pacientes",
                newName: "dataNasc");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "Enderecos",
                newName: "Rua");

            migrationBuilder.RenameColumn(
                name: "DataNascimento",
                table: "Dentistas",
                newName: "dataNasc");

            migrationBuilder.RenameColumn(
                name: "DataNascimento",
                table: "Administrador",
                newName: "dataNasc");

            migrationBuilder.AlterColumn<int>(
                name: "PacienteId",
                table: "Responsavel",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
