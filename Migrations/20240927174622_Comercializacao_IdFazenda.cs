using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class Comercializacao_IdFazenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdFazenda",
                table: "Comercializacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comercializacoes_IdFazenda_idconta",
                table: "Comercializacoes",
                columns: new[] { "IdFazenda", "idconta" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comercializacoes_Fazendas_IdFazenda_idconta",
                table: "Comercializacoes",
                columns: new[] { "IdFazenda", "idconta" },
                principalTable: "Fazendas",
                principalColumns: new[] { "Id", "idconta" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comercializacoes_Fazendas_IdFazenda_idconta",
                table: "Comercializacoes");

            migrationBuilder.DropIndex(
                name: "IX_Comercializacoes_IdFazenda_idconta",
                table: "Comercializacoes");

            migrationBuilder.DropColumn(
                name: "IdFazenda",
                table: "Comercializacoes");
        }
    }
}
