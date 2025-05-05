using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class centrocusto_transacbanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idcentrocusto",
                table: "TransacBanco",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransacBanco_idcentrocusto",
                table: "TransacBanco",
                column: "idcentrocusto");

            migrationBuilder.AddForeignKey(
                name: "FK_TransacBanco_CentroCusto_idcentrocusto",
                table: "TransacBanco",
                column: "idcentrocusto",
                principalTable: "CentroCusto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransacBanco_CentroCusto_idcentrocusto",
                table: "TransacBanco");

            migrationBuilder.DropIndex(
                name: "IX_TransacBanco_idcentrocusto",
                table: "TransacBanco");

            migrationBuilder.DropColumn(
                name: "idcentrocusto",
                table: "TransacBanco");
        }
    }
}
