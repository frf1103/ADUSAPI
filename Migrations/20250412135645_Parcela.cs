using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class Parcela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parcelas",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idassinatura = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    numparcela = table.Column<int>(type: "int", nullable: false),
                    idcaixa = table.Column<int>(type: "int", nullable: true),
                    idformapagto = table.Column<int>(type: "int", nullable: false),
                    datavencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    databaixa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    plataforma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valor = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    comissao = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    descontoplataforma = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    descontoantecipacao = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    valorliquido = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    acrescimos = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    descontos = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    observacao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    idcheckout = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    nossonumero = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    datains = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataup = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcelas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Parcelas_Assinaturas_idassinatura",
                        column: x => x.idassinatura,
                        principalTable: "Assinaturas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcelas_idassinatura",
                table: "Parcelas",
                column: "idassinatura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcelas");
        }
    }
}
