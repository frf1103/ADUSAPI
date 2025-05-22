using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class Convite_Assinatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "idassinatura",
                table: "Convites",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "idformapgto",
                table: "Convites",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Convites_idassinatura",
                table: "Convites",
                column: "idassinatura",
                unique: true,
                filter: "[idassinatura] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Convites_Assinaturas_idassinatura",
                table: "Convites",
                column: "idassinatura",
                principalTable: "Assinaturas",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Convites_Assinaturas_idassinatura",
                table: "Convites");

            migrationBuilder.DropIndex(
                name: "IX_Convites_idassinatura",
                table: "Convites");

            migrationBuilder.DropColumn(
                name: "idassinatura",
                table: "Convites");

            migrationBuilder.DropColumn(
                name: "idformapgto",
                table: "Convites");
        }
    }
}
