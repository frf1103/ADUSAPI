using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaValoresPadraoParceiros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        UPDATE Parceiros
        SET
            isassinante = ISNULL(isassinante, 1),
            isbanco = ISNULL(isbanco, 0),
            isafiliado = ISNULL(isafiliado, 0),
            iscoprodutor = ISNULL(iscoprodutor, 0)
    ");
        }

        /// <inheritdoc />
    }
}