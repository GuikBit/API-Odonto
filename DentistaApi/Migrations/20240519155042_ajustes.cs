using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistaApi.Migrations
{
    /// <inheritdoc />
    public partial class ajustes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anamnese_Pacientes_PacienteId1",
                table: "Anamnese");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pacientes_PacienteId1",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsavel_Pacientes_PacienteId1",
                table: "Responsavel");

            migrationBuilder.DropIndex(
                name: "IX_Responsavel_PacienteId1",
                table: "Responsavel");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_PacienteId1",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Anamnese_PacienteId1",
                table: "Anamnese");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Responsavel");

            migrationBuilder.DropColumn(
                name: "PacienteId1",
                table: "Responsavel");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "PacienteId1",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Anamnese");

            migrationBuilder.DropColumn(
                name: "PacienteId1",
                table: "Anamnese");

            migrationBuilder.CreateIndex(
                name: "IX_Organizacao_EnderecoId",
                table: "Organizacao",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizacao_Endereco_EnderecoId",
                table: "Organizacao",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizacao_Endereco_EnderecoId",
                table: "Organizacao");

            migrationBuilder.DropIndex(
                name: "IX_Organizacao_EnderecoId",
                table: "Organizacao");

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Responsavel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId1",
                table: "Responsavel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Endereco",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId1",
                table: "Endereco",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Anamnese",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId1",
                table: "Anamnese",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responsavel_PacienteId1",
                table: "Responsavel",
                column: "PacienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_PacienteId1",
                table: "Endereco",
                column: "PacienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Anamnese_PacienteId1",
                table: "Anamnese",
                column: "PacienteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Anamnese_Pacientes_PacienteId1",
                table: "Anamnese",
                column: "PacienteId1",
                principalTable: "Pacientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Pacientes_PacienteId1",
                table: "Endereco",
                column: "PacienteId1",
                principalTable: "Pacientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Responsavel_Pacientes_PacienteId1",
                table: "Responsavel",
                column: "PacienteId1",
                principalTable: "Pacientes",
                principalColumn: "Id");
        }
    }
}
