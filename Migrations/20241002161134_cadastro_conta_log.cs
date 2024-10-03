using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class cadastro_conta_log : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "CadastroContas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "CadastroContas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "CadastroContas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datains",
                table: "CadastroContas");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "CadastroContas");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "CadastroContas");
        }
    }
}
