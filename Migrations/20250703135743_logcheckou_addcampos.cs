using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class logcheckou_addcampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "idparcela",
                table: "LogCheckout",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogCheckout_idparcela",
                table: "LogCheckout",
                column: "idparcela");

            migrationBuilder.AddForeignKey(
                name: "FK_LogCheckout_Parcelas_idparcela",
                table: "LogCheckout",
                column: "idparcela",
                principalTable: "Parcelas",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogCheckout_Parcelas_idparcela",
                table: "LogCheckout");

            migrationBuilder.DropIndex(
                name: "IX_LogCheckout_idparcela",
                table: "LogCheckout");

            migrationBuilder.DropColumn(
                name: "idparcela",
                table: "LogCheckout");
        }
    }
}
