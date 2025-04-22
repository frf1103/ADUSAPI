using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class addparametrospagto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idcategoria",
                table: "ParametrosGuru",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idccusto",
                table: "ParametrosGuru",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "idconta",
                table: "ParametrosGuru",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "idparceito",
                table: "ParametrosGuru",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idtransacao",
                table: "ParametrosGuru",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "idcategoria", "idccusto", "idconta", "idparceito", "idtransacao" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "idcategoria", "idccusto", "idconta", "idparceito", "idtransacao" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "idcategoria", "idccusto", "idconta", "idparceito", "idtransacao" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosGuru_idcategoria",
                table: "ParametrosGuru",
                column: "idcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosGuru_idccusto",
                table: "ParametrosGuru",
                column: "idccusto");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosGuru_idconta",
                table: "ParametrosGuru",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosGuru_idparceito",
                table: "ParametrosGuru",
                column: "idparceito");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosGuru_idtransacao",
                table: "ParametrosGuru",
                column: "idtransacao");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_CentroCusto_idccusto",
                table: "ParametrosGuru",
                column: "idccusto",
                principalTable: "CentroCusto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_ContaCorrente_idconta",
                table: "ParametrosGuru",
                column: "idconta",
                principalTable: "ContaCorrente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_Parceiros_idparceito",
                table: "ParametrosGuru",
                column: "idparceito",
                principalTable: "Parceiros",
                principalColumn: "uid");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_PlanoConta_idcategoria",
                table: "ParametrosGuru",
                column: "idcategoria",
                principalTable: "PlanoConta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_Transacoes_idtransacao",
                table: "ParametrosGuru",
                column: "idtransacao",
                principalTable: "Transacoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_CentroCusto_idccusto",
                table: "ParametrosGuru");

            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_ContaCorrente_idconta",
                table: "ParametrosGuru");

            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_Parceiros_idparceito",
                table: "ParametrosGuru");

            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_PlanoConta_idcategoria",
                table: "ParametrosGuru");

            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_Transacoes_idtransacao",
                table: "ParametrosGuru");

            migrationBuilder.DropIndex(
                name: "IX_ParametrosGuru_idcategoria",
                table: "ParametrosGuru");

            migrationBuilder.DropIndex(
                name: "IX_ParametrosGuru_idccusto",
                table: "ParametrosGuru");

            migrationBuilder.DropIndex(
                name: "IX_ParametrosGuru_idconta",
                table: "ParametrosGuru");

            migrationBuilder.DropIndex(
                name: "IX_ParametrosGuru_idparceito",
                table: "ParametrosGuru");

            migrationBuilder.DropIndex(
                name: "IX_ParametrosGuru_idtransacao",
                table: "ParametrosGuru");

            migrationBuilder.DropColumn(
                name: "idcategoria",
                table: "ParametrosGuru");

            migrationBuilder.DropColumn(
                name: "idccusto",
                table: "ParametrosGuru");

            migrationBuilder.DropColumn(
                name: "idconta",
                table: "ParametrosGuru");

            migrationBuilder.DropColumn(
                name: "idparceito",
                table: "ParametrosGuru");

            migrationBuilder.DropColumn(
                name: "idtransacao",
                table: "ParametrosGuru");
        }
    }
}
