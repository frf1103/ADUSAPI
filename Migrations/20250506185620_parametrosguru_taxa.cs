using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class parametrosguru_taxa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idcategoriaant",
                table: "ParametrosGuru",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idcategoriataxa",
                table: "ParametrosGuru",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "idcategoriaant", "idcategoriataxa" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "idcategoriaant", "idcategoriataxa" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "idcategoriaant", "idcategoriataxa" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosGuru_idcategoriaant",
                table: "ParametrosGuru",
                column: "idcategoriaant");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosGuru_idcategoriataxa",
                table: "ParametrosGuru",
                column: "idcategoriataxa");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_PlanoConta_idcategoriaant",
                table: "ParametrosGuru",
                column: "idcategoriaant",
                principalTable: "PlanoConta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametrosGuru_PlanoConta_idcategoriataxa",
                table: "ParametrosGuru",
                column: "idcategoriataxa",
                principalTable: "PlanoConta",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_PlanoConta_idcategoriaant",
                table: "ParametrosGuru");

            migrationBuilder.DropForeignKey(
                name: "FK_ParametrosGuru_PlanoConta_idcategoriataxa",
                table: "ParametrosGuru");

            migrationBuilder.DropIndex(
                name: "IX_ParametrosGuru_idcategoriaant",
                table: "ParametrosGuru");

            migrationBuilder.DropIndex(
                name: "IX_ParametrosGuru_idcategoriataxa",
                table: "ParametrosGuru");

            migrationBuilder.DropColumn(
                name: "idcategoriaant",
                table: "ParametrosGuru");

            migrationBuilder.DropColumn(
                name: "idcategoriataxa",
                table: "ParametrosGuru");
        }
    }
}
