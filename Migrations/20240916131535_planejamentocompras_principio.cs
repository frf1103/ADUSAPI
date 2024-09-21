using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class planejamentocompras_principio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanejamentoCompras_Produtos_IdProduto_idconta",
                table: "PlanejamentoCompras");

            migrationBuilder.DropIndex(
                name: "IX_PlanejamentoCompras_IdProduto_idconta",
                table: "PlanejamentoCompras");

            migrationBuilder.RenameColumn(
                name: "IdProduto",
                table: "PlanejamentoCompras",
                newName: "IdPrincipio");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoCompras_IdPrincipio",
                table: "PlanejamentoCompras",
                column: "IdPrincipio");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanejamentoCompras_PrincipiosAtivos_IdPrincipio",
                table: "PlanejamentoCompras",
                column: "IdPrincipio",
                principalTable: "PrincipiosAtivos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanejamentoCompras_PrincipiosAtivos_IdPrincipio",
                table: "PlanejamentoCompras");

            migrationBuilder.DropIndex(
                name: "IX_PlanejamentoCompras_IdPrincipio",
                table: "PlanejamentoCompras");

            migrationBuilder.RenameColumn(
                name: "IdPrincipio",
                table: "PlanejamentoCompras",
                newName: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoCompras_IdProduto_idconta",
                table: "PlanejamentoCompras",
                columns: new[] { "IdProduto", "idconta" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlanejamentoCompras_Produtos_IdProduto_idconta",
                table: "PlanejamentoCompras",
                columns: new[] { "IdProduto", "idconta" },
                principalTable: "Produtos",
                principalColumns: new[] { "Id", "idconta" });
        }
    }
}
