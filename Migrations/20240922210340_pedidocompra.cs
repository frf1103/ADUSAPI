using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class pedidocompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidoCompra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    idsafra = table.Column<int>(type: "int", nullable: false),
                    idfazenda = table.Column<int>(type: "int", nullable: false),
                    idfornecedor = table.Column<int>(type: "int", nullable: false),
                    idmoeda = table.Column<int>(type: "int", nullable: false),
                    vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    observacao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datains = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataup = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoCompra", x => new { x.id, x.idconta });
                    table.ForeignKey(
                        name: "FK_PedidoCompra_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PedidoCompra_Fazendas_idfazenda_idconta",
                        columns: x => new { x.idfazenda, x.idconta },
                        principalTable: "Fazendas",
                        principalColumns: new[] { "Id", "idconta" });
                    table.ForeignKey(
                        name: "FK_PedidoCompra_Moedas_idmoeda",
                        column: x => x.idmoeda,
                        principalTable: "Moedas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PedidoCompra_Parceiros_idfornecedor_idconta",
                        columns: x => new { x.idfornecedor, x.idconta },
                        principalTable: "Parceiros",
                        principalColumns: new[] { "Id", "idconta" });
                    table.ForeignKey(
                        name: "FK_PedidoCompra_Safras_idsafra_idconta",
                        columns: x => new { x.idsafra, x.idconta },
                        principalTable: "Safras",
                        principalColumns: new[] { "Id", "idconta" });
                });

            migrationBuilder.CreateTable(
                name: "ProdutoCompra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idpedido = table.Column<int>(type: "int", nullable: false),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    idproduto = table.Column<int>(type: "int", nullable: false),
                    qtdcompra = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    preco = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    total = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false),
                    recebido = table.Column<decimal>(type: "decimal(20,2)", precision: 20, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoCompra", x => new { x.id, x.idconta, x.idpedido });
                    table.ForeignKey(
                        name: "FK_ProdutoCompra_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProdutoCompra_PedidoCompra_idpedido_idconta",
                        columns: x => new { x.idpedido, x.idconta },
                        principalTable: "PedidoCompra",
                        principalColumns: new[] { "id", "idconta" });
                    table.ForeignKey(
                        name: "FK_ProdutoCompra_Produtos_idproduto_idconta",
                        columns: x => new { x.idproduto, x.idconta },
                        principalTable: "Produtos",
                        principalColumns: new[] { "Id", "idconta" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCompra_idconta",
                table: "PedidoCompra",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCompra_idfazenda_idconta",
                table: "PedidoCompra",
                columns: new[] { "idfazenda", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCompra_idfornecedor_idconta",
                table: "PedidoCompra",
                columns: new[] { "idfornecedor", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCompra_idmoeda",
                table: "PedidoCompra",
                column: "idmoeda");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCompra_idsafra_idconta",
                table: "PedidoCompra",
                columns: new[] { "idsafra", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCompra_idconta",
                table: "ProdutoCompra",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCompra_idpedido_idconta",
                table: "ProdutoCompra",
                columns: new[] { "idpedido", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCompra_idproduto_idconta",
                table: "ProdutoCompra",
                columns: new[] { "idproduto", "idconta" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoCompra");

            migrationBuilder.DropTable(
                name: "PedidoCompra");
        }
    }
}
