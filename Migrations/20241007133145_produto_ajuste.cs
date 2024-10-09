using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class produto_ajuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanejamentoCompras_PrincipiosAtivos_IdPrincipio",
                table: "PlanejamentoCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosPlanejados_PrincipiosAtivos_IdPrincipioAtivo",
                table: "ProdutosPlanejados");

            migrationBuilder.DropIndex(
                name: "IX_PlanejamentoCompras_IdPrincipio",
                table: "PlanejamentoCompras");

            migrationBuilder.RenameColumn(
                name: "IdPrincipioAtivo",
                table: "ProdutosPlanejados",
                newName: "PrincipioAtivoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutosPlanejados_IdPrincipioAtivo",
                table: "ProdutosPlanejados",
                newName: "IX_ProdutosPlanejados_PrincipioAtivoId");

            migrationBuilder.RenameColumn(
                name: "IdPrincipio",
                table: "PlanejamentoCompras",
                newName: "idproduto");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoCompras_idproduto_idconta",
                table: "PlanejamentoCompras",
                columns: new[] { "idproduto", "idconta" });

            migrationBuilder.Sql("DELETE FROM planejamentocompras where not exists (Select p.id from produtos p where planejamentocompras.idproduto=p.id and planejamentocompras.idconta=p.idconta)");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanejamentoCompras_Produtos_idproduto_idconta",
                table: "PlanejamentoCompras",
                columns: new[] { "idproduto", "idconta" },
                principalTable: "Produtos",
                principalColumns: new[] { "Id", "idconta" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosPlanejados_PrincipiosAtivos_PrincipioAtivoId",
                table: "ProdutosPlanejados",
                column: "PrincipioAtivoId",
                principalTable: "PrincipiosAtivos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanejamentoCompras_Produtos_idproduto_idconta",
                table: "PlanejamentoCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosPlanejados_PrincipiosAtivos_PrincipioAtivoId",
                table: "ProdutosPlanejados");

            migrationBuilder.DropIndex(
                name: "IX_PlanejamentoCompras_idproduto_idconta",
                table: "PlanejamentoCompras");

            migrationBuilder.RenameColumn(
                name: "PrincipioAtivoId",
                table: "ProdutosPlanejados",
                newName: "IdPrincipioAtivo");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutosPlanejados_PrincipioAtivoId",
                table: "ProdutosPlanejados",
                newName: "IX_ProdutosPlanejados_IdPrincipioAtivo");

            migrationBuilder.RenameColumn(
                name: "idproduto",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosPlanejados_PrincipiosAtivos_IdPrincipioAtivo",
                table: "ProdutosPlanejados",
                column: "IdPrincipioAtivo",
                principalTable: "PrincipiosAtivos",
                principalColumn: "Id");
        }
    }
}