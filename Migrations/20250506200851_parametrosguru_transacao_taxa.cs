using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class parametrosguru_transacao_taxa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idtransacaoant",
                table: "ParametrosGuru",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idtransacaotaxa",
                table: "ParametrosGuru",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "idtransacaoant", "idtransacaotaxa" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "idtransacaoant", "idtransacaotaxa" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "idtransacaoant", "idtransacaotaxa" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosGuru_idtransacaoant",
                table: "ParametrosGuru",
                column: "idtransacaoant");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosGuru_idtransacaotaxa",
                table: "ParametrosGuru",
                column: "idtransacaotaxa");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_Transacoes_idtransacaoant",
                table: "ParametrosGuru",
                column: "idtransacaoant",
                principalTable: "Transacoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_Transacoes_idtransacaotaxa",
                table: "ParametrosGuru",
                column: "idtransacaotaxa",
                principalTable: "Transacoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_Transacoes_idtransacaoant",
                table: "ParametrosGuru");

            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_Transacoes_idtransacaotaxa",
                table: "ParametrosGuru");

            migrationBuilder.DropIndex(
                name: "IX_ParametrosGuru_idtransacaoant",
                table: "ParametrosGuru");

            migrationBuilder.DropIndex(
                name: "IX_ParametrosGuru_idtransacaotaxa",
                table: "ParametrosGuru");

            migrationBuilder.DropColumn(
                name: "idtransacaoant",
                table: "ParametrosGuru");

            migrationBuilder.DropColumn(
                name: "idtransacaotaxa",
                table: "ParametrosGuru");
        }
    }
}
