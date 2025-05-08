using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class parametrosguru_comiss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idcategoriacomiss",
                table: "ParametrosGuru",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idtransacaocomiss",
                table: "ParametrosGuru",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "idcategoriacomiss", "idtransacaocomiss" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "idcategoriacomiss", "idtransacaocomiss" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "idcategoriacomiss", "idtransacaocomiss" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosGuru_idcategoriacomiss",
                table: "ParametrosGuru",
                column: "idcategoriacomiss");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosGuru_idtransacaocomiss",
                table: "ParametrosGuru",
                column: "idtransacaocomiss");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_PlanoConta_idcategoriacomiss",
                table: "ParametrosGuru",
                column: "idcategoriacomiss",
                principalTable: "PlanoConta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_Transacoes_idtransacaocomiss",
                table: "ParametrosGuru",
                column: "idtransacaocomiss",
                principalTable: "Transacoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_PlanoConta_idcategoriacomiss",
                table: "ParametrosGuru");

            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_Transacoes_idtransacaocomiss",
                table: "ParametrosGuru");

            migrationBuilder.DropIndex(
                name: "IX_ParametrosGuru_idcategoriacomiss",
                table: "ParametrosGuru");

            migrationBuilder.DropIndex(
                name: "IX_ParametrosGuru_idtransacaocomiss",
                table: "ParametrosGuru");

            migrationBuilder.DropColumn(
                name: "idcategoriacomiss",
                table: "ParametrosGuru");

            migrationBuilder.DropColumn(
                name: "idtransacaocomiss",
                table: "ParametrosGuru");
        }
    }
}
