using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class parametrosguru : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParametrosGuru",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    token = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ultdata = table.Column<DateTime>(type: "datetime2", nullable: false),
                    urlsub = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    urltransac = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosGuru", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "ParametrosGuru",
                columns: new[] { "id", "token", "ultdata", "urlsub", "urltransac" },
                values: new object[] { 1, "9e883bc2-e356-440e-b28b-327532ace5d2|LuSXsELCchQMjGN0A9CICbQJCLwhglsstYRPPVr57fb50393", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://digitalmanager.guru/api/v2/subscriptions", "https://digitalmanager.guru/api/v2/transactions" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParametrosGuru");
        }
    }
}
