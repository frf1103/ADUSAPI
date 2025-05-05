using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class parceiro_locais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parceiros_Municipios_Cidade",
                table: "Parceiros");

            migrationBuilder.DropForeignKey(
                name: "FK_Parceiros_UFs_UF",
                table: "Parceiros");

            migrationBuilder.RenameColumn(
                name: "UF",
                table: "Parceiros",
                newName: "idUF");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "Parceiros",
                newName: "idCidade");

            migrationBuilder.RenameIndex(
                name: "IX_Parceiros_UF",
                table: "Parceiros",
                newName: "IX_Parceiros_idUF");

            migrationBuilder.RenameIndex(
                name: "IX_Parceiros_Cidade",
                table: "Parceiros",
                newName: "IX_Parceiros_idCidade");

            migrationBuilder.AddForeignKey(
                name: "FK_Parceiros_Municipios_idCidade",
                table: "Parceiros",
                column: "idCidade",
                principalTable: "Municipios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parceiros_UFs_idUF",
                table: "Parceiros",
                column: "idUF",
                principalTable: "UFs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parceiros_Municipios_idCidade",
                table: "Parceiros");

            migrationBuilder.DropForeignKey(
                name: "FK_Parceiros_UFs_idUF",
                table: "Parceiros");

            migrationBuilder.RenameColumn(
                name: "idUF",
                table: "Parceiros",
                newName: "UF");

            migrationBuilder.RenameColumn(
                name: "idCidade",
                table: "Parceiros",
                newName: "Cidade");

            migrationBuilder.RenameIndex(
                name: "IX_Parceiros_idUF",
                table: "Parceiros",
                newName: "IX_Parceiros_UF");

            migrationBuilder.RenameIndex(
                name: "IX_Parceiros_idCidade",
                table: "Parceiros",
                newName: "IX_Parceiros_Cidade");

            migrationBuilder.AddForeignKey(
                name: "FK_Parceiros_Municipios_Cidade",
                table: "Parceiros",
                column: "Cidade",
                principalTable: "Municipios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parceiros_UFs_UF",
                table: "Parceiros",
                column: "UF",
                principalTable: "UFs",
                principalColumn: "Id");
        }
    }
}
