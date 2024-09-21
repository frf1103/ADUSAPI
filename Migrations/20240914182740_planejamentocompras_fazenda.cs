using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class planejamentocompras_fazenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdFazenda",
                table: "PlanejamentoCompras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoCompras_IdFazenda_idconta",
                table: "PlanejamentoCompras",
                columns: new[] { "IdFazenda", "idconta" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlanejamentoCompras_Fazendas_IdFazenda_idconta",
                table: "PlanejamentoCompras",
                columns: new[] { "IdFazenda", "idconta" },
                principalTable: "Fazendas",
                principalColumns: new[] { "Id", "idconta" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanejamentoCompras_Fazendas_IdFazenda_idconta",
                table: "PlanejamentoCompras");

            migrationBuilder.DropIndex(
                name: "IX_PlanejamentoCompras_IdFazenda_idconta",
                table: "PlanejamentoCompras");

            migrationBuilder.DropColumn(
                name: "IdFazenda",
                table: "PlanejamentoCompras");
        }
    }
}
