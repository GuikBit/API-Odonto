using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistaApi.Migrations
{
    /// <inheritdoc />
    public partial class PagamentoAjuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoAparelho",
                table: "ConsultaEspecialidades");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataDoPagamento",
                table: "Pagamentos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataDoPagamento",
                table: "Pagamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoAparelho",
                table: "ConsultaEspecialidades",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
