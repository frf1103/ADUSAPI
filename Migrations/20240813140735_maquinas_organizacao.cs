using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class maquinas_organizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idorganizacao",
                table: "Maquinas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_idorganizacao",
                table: "Maquinas",
                column: "idorganizacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Maquinas_Organizacoes_idorganizacao",
                table: "Maquinas",
                column: "idorganizacao",
                principalTable: "Organizacoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maquinas_Organizacoes_idorganizacao",
                table: "Maquinas");

            migrationBuilder.DropIndex(
                name: "IX_Maquinas_idorganizacao",
                table: "Maquinas");

            migrationBuilder.DropColumn(
                name: "idorganizacao",
                table: "Maquinas");
        }
    }
}
