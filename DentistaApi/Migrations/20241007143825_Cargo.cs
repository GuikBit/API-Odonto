using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistaApi.Migrations
{
    /// <inheritdoc />
    public partial class Cargo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bonificacao",
                table: "Cargos");

            //migrationBuilder.RenameColumn(
            //    name: "Titulo",
            //    table: "Cargos",
            //    newName: "Requisitos");

            //migrationBuilder.RenameColumn(
            //    name: "Salario",
            //    table: "Cargos",
            //    newName: "SalarioBase");

            //migrationBuilder.RenameColumn(
            //    name: "HrSemanais",
            //    table: "Cargos",
            //    newName: "Nome");

            migrationBuilder.AlterColumn<int>(
                name: "IdUserUpdade",
                table: "Cargos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Cargos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataUpdate",
                table: "Cargos",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<string>(
                name: "CargaHoraria",
                table: "Cargos",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "GymPass",
                table: "Cargos",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NivelHierarquico",
                table: "Cargos",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "PLR",
                table: "Cargos",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PlanoSaude",
                table: "Cargos",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Premiacao",
                table: "Cargos",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Cargos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ValeAR",
                table: "Cargos",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ValeTrans",
                table: "Cargos",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargaHoraria",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "GymPass",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "NivelHierarquico",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "PLR",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "PlanoSaude",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "Premiacao",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "ValeAR",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "ValeTrans",
                table: "Cargos");

            //migrationBuilder.RenameColumn(
            //    name: "SalarioBase",
            //    table: "Cargos",
            //    newName: "Salario");

            //migrationBuilder.RenameColumn(
            //    name: "Requisitos",
            //    table: "Cargos",
            //    newName: "Titulo");

            //migrationBuilder.RenameColumn(
            //    name: "Nome",
            //    table: "Cargos",
            //    newName: "HrSemanais");

            migrationBuilder.AlterColumn<int>(
                name: "IdUserUpdade",
                table: "Cargos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Cargos",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataUpdate",
                table: "Cargos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Bonificacao",
                table: "Cargos",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
