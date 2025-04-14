using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class assinatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assinaturas",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idparceiro = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    qtd = table.Column<int>(type: "int", nullable: false),
                    preco = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    valor = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    datavenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idformapagto = table.Column<int>(type: "int", nullable: false),
                    idplataforma = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    datains = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataup = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinaturas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Parceiros_idparceiro",
                        column: x => x.idparceiro,
                        principalTable: "Parceiros",
                        principalColumn: "uid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_idparceiro",
                table: "Assinaturas",
                column: "idparceiro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assinaturas");
        }
    }
}
