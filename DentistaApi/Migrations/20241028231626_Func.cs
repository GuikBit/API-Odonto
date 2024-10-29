using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistaApi.Migrations
{
    /// <inheritdoc />
    public partial class Func : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrador_Organizacao_OrganizacaoId",
                table: "Administrador");

            migrationBuilder.DropForeignKey(
                name: "FK_Dentistas_Organizacao_OrganizacaoId",
                table: "Dentistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Organizacao_OrganizacaoId",
                table: "Funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Organizacao_OrganizacaoId",
                table: "Pacientes");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizacaoId",
                table: "Pacientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizacaoId",
                table: "Funcionarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizacaoId",
                table: "Dentistas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "ValorPremiacao",
                table: "Cargos",
                type: "double",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizacaoId",
                table: "Administrador",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrador_Organizacao_OrganizacaoId",
                table: "Administrador",
                column: "OrganizacaoId",
                principalTable: "Organizacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dentistas_Organizacao_OrganizacaoId",
                table: "Dentistas",
                column: "OrganizacaoId",
                principalTable: "Organizacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Organizacao_OrganizacaoId",
                table: "Funcionarios",
                column: "OrganizacaoId",
                principalTable: "Organizacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Organizacao_OrganizacaoId",
                table: "Pacientes",
                column: "OrganizacaoId",
                principalTable: "Organizacao",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrador_Organizacao_OrganizacaoId",
                table: "Administrador");

            migrationBuilder.DropForeignKey(
                name: "FK_Dentistas_Organizacao_OrganizacaoId",
                table: "Dentistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Organizacao_OrganizacaoId",
                table: "Funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Organizacao_OrganizacaoId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "ValorPremiacao",
                table: "Cargos");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizacaoId",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizacaoId",
                table: "Funcionarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizacaoId",
                table: "Dentistas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizacaoId",
                table: "Administrador",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Administrador_Organizacao_OrganizacaoId",
                table: "Administrador",
                column: "OrganizacaoId",
                principalTable: "Organizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dentistas_Organizacao_OrganizacaoId",
                table: "Dentistas",
                column: "OrganizacaoId",
                principalTable: "Organizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Organizacao_OrganizacaoId",
                table: "Funcionarios",
                column: "OrganizacaoId",
                principalTable: "Organizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Organizacao_OrganizacaoId",
                table: "Pacientes",
                column: "OrganizacaoId",
                principalTable: "Organizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
