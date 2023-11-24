using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistaApi.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDentista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dentistas_Especialidades_Id",
                table: "Dentistas");

            migrationBuilder.AddColumn<int>(
                name: "EspecialidadeId",
                table: "Dentistas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dentistas_EspecialidadeId",
                table: "Dentistas",
                column: "EspecialidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dentistas_Especialidades_EspecialidadeId",
                table: "Dentistas",
                column: "EspecialidadeId",
                principalTable: "Especialidades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dentistas_Especialidades_EspecialidadeId",
                table: "Dentistas");

            migrationBuilder.DropIndex(
                name: "IX_Dentistas_EspecialidadeId",
                table: "Dentistas");

            migrationBuilder.DropColumn(
                name: "EspecialidadeId",
                table: "Dentistas");

            migrationBuilder.AddForeignKey(
                name: "FK_Dentistas_Especialidades_Id",
                table: "Dentistas",
                column: "Id",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
