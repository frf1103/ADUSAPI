using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class transacbanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransacBanco",
                columns: table => new
                {
                    idtransacbanco = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    idbanco = table.Column<int>(type: "int", nullable: false),
                    idtransacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacBanco", x => new { x.idtransacbanco, x.idbanco });
                    table.ForeignKey(
                        name: "FK_TransacBanco_Bancos_idbanco",
                        column: x => x.idbanco,
                        principalTable: "Bancos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransacBanco_Transacoes_idtransacao",
                        column: x => x.idtransacao,
                        principalTable: "Transacoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransacBanco_idbanco",
                table: "TransacBanco",
                column: "idbanco");

            migrationBuilder.CreateIndex(
                name: "IX_TransacBanco_idtransacao",
                table: "TransacBanco",
                column: "idtransacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransacBanco");
        }
    }
}
