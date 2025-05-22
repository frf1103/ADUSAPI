using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class convites_perfilparceiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "idcoprodutor",
                table: "Parceiros",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iduser",
                table: "Parceiros",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isafiliado",
                table: "Parceiros",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isassinante",
                table: "Parceiros",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "isbanco",
                table: "Parceiros",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "iscoprodutor",
                table: "Parceiros",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "percomissao",
                table: "Parceiros",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Convites",
                columns: table => new
                {
                    IdConvite = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdAfiliado = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdPlataforma = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convites", x => x.IdConvite);
                    table.ForeignKey(
                        name: "FK_Convites_Parceiros_IdAfiliado",
                        column: x => x.IdAfiliado,
                        principalTable: "Parceiros",
                        principalColumn: "uid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parceiros_idcoprodutor",
                table: "Parceiros",
                column: "idcoprodutor");

            migrationBuilder.CreateIndex(
                name: "IX_Convites_IdAfiliado",
                table: "Convites",
                column: "IdAfiliado");

            migrationBuilder.AddForeignKey(
                name: "FK_Parceiros_Parceiros_idcoprodutor",
                table: "Parceiros",
                column: "idcoprodutor",
                principalTable: "Parceiros",
                principalColumn: "uid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parceiros_Parceiros_idcoprodutor",
                table: "Parceiros");

            migrationBuilder.DropTable(
                name: "Convites");

            migrationBuilder.DropIndex(
                name: "IX_Parceiros_idcoprodutor",
                table: "Parceiros");

            migrationBuilder.DropColumn(
                name: "idcoprodutor",
                table: "Parceiros");

            migrationBuilder.DropColumn(
                name: "iduser",
                table: "Parceiros");

            migrationBuilder.DropColumn(
                name: "isafiliado",
                table: "Parceiros");

            migrationBuilder.DropColumn(
                name: "isassinante",
                table: "Parceiros");

            migrationBuilder.DropColumn(
                name: "isbanco",
                table: "Parceiros");

            migrationBuilder.DropColumn(
                name: "iscoprodutor",
                table: "Parceiros");

            migrationBuilder.DropColumn(
                name: "percomissao",
                table: "Parceiros");
        }
    }
}
