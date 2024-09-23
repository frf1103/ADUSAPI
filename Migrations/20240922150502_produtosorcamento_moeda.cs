using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class produtosorcamento_moeda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idmoeda",
                table: "ProdutosOrcamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosOrcamento_idmoeda",
                table: "ProdutosOrcamento",
                column: "idmoeda");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosOrcamento_Moedas_idmoeda",
                table: "ProdutosOrcamento",
                column: "idmoeda",
                principalTable: "Moedas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosOrcamento_Moedas_idmoeda",
                table: "ProdutosOrcamento");

            migrationBuilder.DropIndex(
                name: "IX_ProdutosOrcamento_idmoeda",
                table: "ProdutosOrcamento");

            migrationBuilder.DropColumn(
                name: "idmoeda",
                table: "ProdutosOrcamento");
        }
    }
}
