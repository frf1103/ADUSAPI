using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class controle_log : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "UsuarioConta",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "UsuarioConta",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uidlog",
                table: "UsuarioConta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "Talhoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "Talhoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "Talhoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "Safras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "Safras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "Safras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "Produtos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "Produtos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "PrincipiosAtivos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "PrincipiosAtivos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "PrincipiosAtivos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "Parceiros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "Parceiros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "Parceiros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "Organizacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "Organizacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "Organizacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "OrganizacaoUsuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "OrganizacaoUsuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uidlog",
                table: "OrganizacaoUsuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "MaquinasPlanejadas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "MaquinasPlanejadas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "MaquinasPlanejadas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "MaquinasParametros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "MaquinasParametros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "MaquinasParametros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "Maquinas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "Maquinas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "Maquinas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "ConfigAreas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "ConfigAreas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "ConfigAreas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "AnosAgricolas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "AnosAgricolas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "AnosAgricolas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "farmPlannerLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datalog = table.Column<DateTime>(type: "datetime2", nullable: false),
                    transacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_farmPlannerLogs", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "farmPlannerLogs");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "UsuarioConta");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "UsuarioConta");

            migrationBuilder.DropColumn(
                name: "uidlog",
                table: "UsuarioConta");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "Talhoes");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "Talhoes");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "Talhoes");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "Safras");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "Safras");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "Safras");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "PrincipiosAtivos");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "PrincipiosAtivos");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "PrincipiosAtivos");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "Parceiros");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "Parceiros");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "Parceiros");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "Organizacoes");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "Organizacoes");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "Organizacoes");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "OrganizacaoUsuario");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "OrganizacaoUsuario");

            migrationBuilder.DropColumn(
                name: "uidlog",
                table: "OrganizacaoUsuario");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "MaquinasPlanejadas");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "MaquinasPlanejadas");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "MaquinasPlanejadas");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "MaquinasParametros");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "MaquinasParametros");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "MaquinasParametros");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "Maquinas");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "Maquinas");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "Maquinas");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "ConfigAreas");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "ConfigAreas");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "ConfigAreas");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "AnosAgricolas");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "AnosAgricolas");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "AnosAgricolas");
        }
    }
}
