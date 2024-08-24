using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class controle_log_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Safras",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "Safras",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "Safras",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "ProdutosPlanejados",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "ProdutosPlanejados",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "ProdutosPlanejados",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "ProdutosOrcamento",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "ProdutosOrcamento",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "ProdutosOrcamento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "Produtos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "Produtos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "PrincipiosAtivos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "PrincipiosAtivos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "PrincipiosAtivos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "PreferUsu",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "PreferUsu",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uidlog",
                table: "PreferUsu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "PlanejamentoOperacoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "PlanejamentoOperacoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "PlanejamentoOperacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "PlanejamentoCompras",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "PlanejamentoCompras",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "PlanejamentoCompras",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Parceiros",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "Parceiros",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "Parceiros",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Organizacoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "Organizacoes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "Organizacoes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "OrcamentoProdutos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "OrcamentoProdutos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "OrcamentoProdutos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "orcamentocustosindiretos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "orcamentocustosindiretos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "orcamentocustosindiretos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "Operacoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "Operacoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "Operacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "ModelosParametros",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "ModelosParametros",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "ModelosParametros",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "MaquinasPlanejadas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "MaquinasPlanejadas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "MaquinasPlanejadas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "MaquinasParametros",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "MaquinasParametros",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "MaquinasParametros",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Maquinas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "Maquinas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "Maquinas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "GruposContas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "GruposContas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "GruposContas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "FinanceiroConta",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "FinanceiroConta",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "FinanceiroConta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "Fazendas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "Fazendas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "Fazendas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "entregaContratos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "entregaContratos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "entregaContratos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datains",
                table: "Comercializacoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataup",
                table: "Comercializacoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "Comercializacoes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datains",
                table: "ProdutosPlanejados");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "ProdutosPlanejados");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "ProdutosPlanejados");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "ProdutosOrcamento");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "ProdutosOrcamento");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "ProdutosOrcamento");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "PreferUsu");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "PreferUsu");

            migrationBuilder.DropColumn(
                name: "uidlog",
                table: "PreferUsu");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "PlanejamentoOperacoes");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "PlanejamentoOperacoes");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "PlanejamentoOperacoes");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "PlanejamentoCompras");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "PlanejamentoCompras");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "PlanejamentoCompras");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "OrcamentoProdutos");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "OrcamentoProdutos");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "OrcamentoProdutos");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "orcamentocustosindiretos");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "orcamentocustosindiretos");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "orcamentocustosindiretos");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "Operacoes");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "Operacoes");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "Operacoes");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "ModelosParametros");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "ModelosParametros");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "ModelosParametros");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "GruposContas");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "GruposContas");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "GruposContas");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "FinanceiroConta");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "FinanceiroConta");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "FinanceiroConta");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "Fazendas");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "Fazendas");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "Fazendas");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "entregaContratos");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "entregaContratos");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "entregaContratos");

            migrationBuilder.DropColumn(
                name: "datains",
                table: "Comercializacoes");

            migrationBuilder.DropColumn(
                name: "dataup",
                table: "Comercializacoes");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "Comercializacoes");

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Safras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "Safras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "Safras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "Produtos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "Produtos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "PrincipiosAtivos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "PrincipiosAtivos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "PrincipiosAtivos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Parceiros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "Parceiros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "Parceiros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Organizacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "Organizacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "Organizacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "MaquinasPlanejadas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "MaquinasPlanejadas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "MaquinasPlanejadas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "MaquinasParametros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "MaquinasParametros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "MaquinasParametros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Maquinas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataup",
                table: "Maquinas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "datains",
                table: "Maquinas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
