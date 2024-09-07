using FarmPlannerAPICore.Models.Localidades;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text.Json;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class load_uf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string fileNameUF = "estados.json";
            string fileNamemun = "municipios.json";
            string jsonStringuf = File.ReadAllText(fileNameUF);
            string jsonStringmun = File.ReadAllText(fileNamemun);
            var ufs = JsonSerializer.Deserialize<List<ImpUF>>(jsonStringuf);
            var muns = JsonSerializer.Deserialize<List<ImpMun>>(jsonStringmun);
            foreach (var y in ufs)
            {
                var sql = @"insert into Ufs(Nome,Sigla,CodigoIBGE) values('" + y.nome + "','" + y.uf + "','" + y.codigo_uf.ToString() + "')";

                migrationBuilder.Sql(sql);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}