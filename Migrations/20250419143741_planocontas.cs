using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class planocontas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanoConta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdMae = table.Column<int>(type: "int", nullable: true),
                    Sinal = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoConta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanoConta_PlanoConta_IdMae",
                        column: x => x.IdMae,
                        principalTable: "PlanoConta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanoConta_IdMae",
                table: "PlanoConta",
                column: "IdMae");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanoConta");
        }
    }
}
