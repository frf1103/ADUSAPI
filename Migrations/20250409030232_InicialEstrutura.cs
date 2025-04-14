using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class InicialEstrutura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ADUSLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datalog = table.Column<DateTime>(type: "datetime2", nullable: false),
                    transacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADUSLogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Moedas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moedas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UFs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CodigoIBGE = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UFs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CotacoesMoeda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMoeda = table.Column<int>(type: "int", nullable: false),
                    CotacaoData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CotacaoValor = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotacoesMoeda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CotacoesMoeda_Moedas_IdMoeda",
                        column: x => x.IdMoeda,
                        principalTable: "Moedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CodigoIBGE = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    IdUF = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipios_UFs_IdUF",
                        column: x => x.IdUF,
                        principalTable: "UFs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parceiros",
                columns: table => new
                {
                    uid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Fantasia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipodePessoa = table.Column<int>(type: "int", nullable: false),
                    Registro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UF = table.Column<int>(type: "int", nullable: false),
                    Cidade = table.Column<int>(type: "int", nullable: false),
                    Profissao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoCivil = table.Column<int>(type: "int", nullable: false),
                    IdRepresentante = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DtNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    datains = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dataup = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<int>(type: "int", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parceiros", x => x.uid);
                    table.ForeignKey(
                        name: "FK_Parceiros_Municipios_Cidade",
                        column: x => x.Cidade,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parceiros_Parceiros_IdRepresentante",
                        column: x => x.IdRepresentante,
                        principalTable: "Parceiros",
                        principalColumn: "uid");
                    table.ForeignKey(
                        name: "FK_Parceiros_UFs_UF",
                        column: x => x.UF,
                        principalTable: "UFs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CotacoesMoeda_IdMoeda",
                table: "CotacoesMoeda",
                column: "IdMoeda");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_IdUF",
                table: "Municipios",
                column: "IdUF");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiros_Cidade",
                table: "Parceiros",
                column: "Cidade");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiros_IdRepresentante",
                table: "Parceiros",
                column: "IdRepresentante");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiros_UF",
                table: "Parceiros",
                column: "UF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADUSLogs");

            migrationBuilder.DropTable(
                name: "CotacoesMoeda");

            migrationBuilder.DropTable(
                name: "Parceiros");

            migrationBuilder.DropTable(
                name: "Moedas");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "UFs");
        }
    }
}
