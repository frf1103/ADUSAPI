using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class cartaoassinatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cartoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAssinatura = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IdToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UltimosDigitos = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Bandeira = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cartoes_Assinaturas_IdAssinatura",
                        column: x => x.IdAssinatura,
                        principalTable: "Assinaturas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cartoes_IdAssinatura",
                table: "cartoes",
                column: "IdAssinatura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartoes");
        }
    }
}
