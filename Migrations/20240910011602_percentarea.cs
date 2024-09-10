using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class percentarea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaPercent",
                table: "ProdutosPlanejados");

            migrationBuilder.AddColumn<decimal>(
                name: "Percentual",
                table: "PlanejamentoOperacoes",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentual",
                table: "PlanejamentoOperacoes");

            migrationBuilder.AddColumn<decimal>(
                name: "AreaPercent",
                table: "ProdutosPlanejados",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
