using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistaApi.Migrations
{
    /// <inheritdoc />
    public partial class AjustePagamemto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Pagamentos_PagamentoId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "DataDoPagamento",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "FormaDePagamento",
                table: "Pagamentos");

            migrationBuilder.AddColumn<int>(
                name: "qtdParcela",
                table: "Pagamentos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PagamentoId",
                table: "Consultas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Parcela",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Pago = table.Column<bool>(type: "INTEGER", nullable: false),
                    ValorParcela = table.Column<double>(type: "REAL", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataVencimento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FormaDePagamento = table.Column<int>(type: "INTEGER", nullable: true),
                    PagamentoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcela_Pagamentos_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Pagamentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcela_PagamentoId",
                table: "Parcela",
                column: "PagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Pagamentos_PagamentoId",
                table: "Consultas",
                column: "PagamentoId",
                principalTable: "Pagamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Pagamentos_PagamentoId",
                table: "Consultas");

            migrationBuilder.DropTable(
                name: "Parcela");

            migrationBuilder.DropColumn(
                name: "qtdParcela",
                table: "Pagamentos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDoPagamento",
                table: "Pagamentos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormaDePagamento",
                table: "Pagamentos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PagamentoId",
                table: "Consultas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Pagamentos_PagamentoId",
                table: "Consultas",
                column: "PagamentoId",
                principalTable: "Pagamentos",
                principalColumn: "Id");
        }
    }
}
