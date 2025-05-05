using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class categoria_transacbanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idcategoria",
                table: "TransacBanco",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransacBanco_idcategoria",
                table: "TransacBanco",
                column: "idcategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_TransacBanco_PlanoConta_idcategoria",
                table: "TransacBanco",
                column: "idcategoria",
                principalTable: "PlanoConta",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransacBanco_PlanoConta_idcategoria",
                table: "TransacBanco");

            migrationBuilder.DropIndex(
                name: "IX_TransacBanco_idcategoria",
                table: "TransacBanco");

            migrationBuilder.DropColumn(
                name: "idcategoria",
                table: "TransacBanco");
        }
    }
}
