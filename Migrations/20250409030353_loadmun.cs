﻿using ADUSAPICore.Models.Localidades;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text.Json;

#nullable disable

namespace ADUSAPI.Migrations
{
    /// <inheritdoc />
    public partial class loadmun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string fileNamemun = "municipios.json";

            string jsonStringmun = File.ReadAllText(fileNamemun);

            var muns = JsonSerializer.Deserialize<List<ImpMun>>(jsonStringmun);

            foreach (var y in muns)
            {
                var sql = @"insert into municipios(Nome,CodigoIBGE,IdUf) select '" + y.nome + "','" + y.codigo_ibge + "',u.id from ufs u where codigoibge='" + y.codigo_uf + "'";
                migrationBuilder.Sql(sql);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}