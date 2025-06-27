using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class logcheckout_Null_erro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogCheckout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCliente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IpOrigem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoOperacao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UrlRequisicao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PayloadEnviado = table.Column<bool>(type: "bit", nullable: false),
                    RetornoApi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusHttp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Erro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogCheckout", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogCheckout");
        }
    }
}
