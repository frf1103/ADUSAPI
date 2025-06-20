using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class afiliadoassinatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "idafiliado",
                table: "Assinaturas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_idafiliado",
                table: "Assinaturas",
                column: "idafiliado");

            migrationBuilder.AddForeignKey(
                name: "FK_Assinaturas_Parceiros_idafiliado",
                table: "Assinaturas",
                column: "idafiliado",
                principalTable: "Parceiros",
                principalColumn: "uid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assinaturas_Parceiros_idafiliado",
                table: "Assinaturas");

            migrationBuilder.DropIndex(
                name: "IX_Assinaturas_idafiliado",
                table: "Assinaturas");

            migrationBuilder.DropColumn(
                name: "idafiliado",
                table: "Assinaturas");
        }
    }
}
