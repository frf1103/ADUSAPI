using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class parametrospagto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "urltransac",
                table: "ParametrosGuru",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "urlsub",
                table: "ParametrosGuru",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "token",
                table: "ParametrosGuru",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "ParametrosGuru",
                columns: new[] { "id", "token", "ultdata", "urlsub", "urltransac" },
                values: new object[,]
                {
                    { 2, "$aact_prod_000MzkwODA2MWY2OGM3MWRlMDU2NWM3MzJlNzZmNGZhZGY6OmIyNmM2YWIzLThmOGUtNDY5Mi1hNDNkLWJiNDk4YTRmNGNjOTo6JGFhY2hfZTI5MTFhMGMtYjdkNi00MzhlLWI2OTEtOTYxNzYzMmI2NDBk", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), " ", "https://api.asaas.com/v3/payments" },
                    { 3, "sk_871c3d7c606a4be3bd48d4f86b68c58f", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), " ", "https://api.pagar.me/core/v5/payables" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParametrosGuru",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "urltransac",
                table: "ParametrosGuru",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "urlsub",
                table: "ParametrosGuru",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "token",
                table: "ParametrosGuru",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
