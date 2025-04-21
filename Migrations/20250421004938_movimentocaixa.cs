using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class movimentocaixa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimentoCaixa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTransacao = table.Column<int>(type: "int", nullable: false),
                    IdCentroCusto = table.Column<int>(type: "int", nullable: false),
                    IdContaCorrente = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    Sinal = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataMov = table.Column<DateTime>(type: "datetime", nullable: false),
                    idparceiro = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentoCaixa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentoCaixa_CentroCusto_IdCentroCusto",
                        column: x => x.IdCentroCusto,
                        principalTable: "CentroCusto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimentoCaixa_ContaCorrente_IdContaCorrente",
                        column: x => x.IdContaCorrente,
                        principalTable: "ContaCorrente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimentoCaixa_Parceiros_idparceiro",
                        column: x => x.idparceiro,
                        principalTable: "Parceiros",
                        principalColumn: "uid");
                    table.ForeignKey(
                        name: "FK_MovimentoCaixa_PlanoConta_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "PlanoConta",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimentoCaixa_Transacoes_IdTransacao",
                        column: x => x.IdTransacao,
                        principalTable: "Transacoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoCaixa_IdCategoria",
                table: "MovimentoCaixa",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoCaixa_IdCentroCusto",
                table: "MovimentoCaixa",
                column: "IdCentroCusto");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoCaixa_IdContaCorrente",
                table: "MovimentoCaixa",
                column: "IdContaCorrente");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoCaixa_idparceiro",
                table: "MovimentoCaixa",
                column: "idparceiro");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoCaixa_IdTransacao",
                table: "MovimentoCaixa",
                column: "IdTransacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentoCaixa");
        }
    }
}
