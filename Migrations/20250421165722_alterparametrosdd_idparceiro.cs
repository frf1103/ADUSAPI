using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class alterparametrosdd_idparceiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_Parceiros_idparceito",
                table: "ParametrosGuru");

            migrationBuilder.RenameColumn(
                name: "idparceito",
                table: "ParametrosGuru",
                newName: "idparceiro");

            migrationBuilder.RenameIndex(
                name: "IX_ParametrosGuru_idparceito",
                table: "ParametrosGuru",
                newName: "IX_ParametrosGuru_idparceiro");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_Parceiros_idparceiro",
                table: "ParametrosGuru",
                column: "idparceiro",
                principalTable: "Parceiros",
                principalColumn: "uid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_Parceiros_idparceiro",
                table: "ParametrosGuru");

            migrationBuilder.RenameColumn(
                name: "idparceiro",
                table: "ParametrosGuru",
                newName: "idparceito");

            migrationBuilder.RenameIndex(
                name: "IX_ParametrosGuru_idparceiro",
                table: "ParametrosGuru",
                newName: "IX_ParametrosGuru_idparceito");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_Parceiros_idparceito",
                table: "ParametrosGuru",
                column: "idparceito",
                principalTable: "Parceiros",
                principalColumn: "uid");
        }
    }
}
