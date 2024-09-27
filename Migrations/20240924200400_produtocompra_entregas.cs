using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class produtocompra_entregas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entregascompras",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idprodutopedido = table.Column<int>(type: "int", nullable: false),
                    idpedido = table.Column<int>(type: "int", nullable: false),
                    idproduto = table.Column<int>(type: "int", nullable: false),
                    idunidentrega = table.Column<int>(type: "int", nullable: false),
                    conversor = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    dataentrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    documento = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    qtd = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datains = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataup = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entregascompras", x => x.id);
                    table.ForeignKey(
                        name: "FK_entregascompras_ProdutoCompra_idprodutopedido_idconta_idpedido",
                        columns: x => new { x.idprodutopedido, x.idconta, x.idpedido },
                        principalTable: "ProdutoCompra",
                        principalColumns: new[] { "id", "idconta", "idpedido" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entregascompras_Produtos_idproduto_idconta",
                        columns: x => new { x.idproduto, x.idconta },
                        principalTable: "Produtos",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entregascompras_idproduto_idconta",
                table: "entregascompras",
                columns: new[] { "idproduto", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_entregascompras_idprodutopedido_idconta_idpedido",
                table: "entregascompras",
                columns: new[] { "idprodutopedido", "idconta", "idpedido" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entregascompras");
        }
    }
}
