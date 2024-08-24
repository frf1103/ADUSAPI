using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class estrutura_inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassesContas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoClasseConta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassesContas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    ContaGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    representanteid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ativa = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Culturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnidadeProdutiva = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NomeProduto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiasEstimadosEmergencia = table.Column<int>(type: "int", nullable: false),
                    CodigoExterno = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Culturas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GruposProdutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposProdutos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarcasMaquinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasMaquinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moedas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moedas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regioes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mascara = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regioes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tecnologias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnologias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposOperacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposOperacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UFs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CodigoIBGE = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UFs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssinaturaConta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    plano = table.Column<int>(type: "int", nullable: false),
                    dataassinatura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataexpiracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssinaturaConta", x => x.id);
                    table.ForeignKey(
                        name: "FK_AssinaturaConta_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FinanceiroConta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    emissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    datapagto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    desconto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    valorpago = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    obs = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceiroConta", x => x.id);
                    table.ForeignKey(
                        name: "FK_FinanceiroConta_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Organizacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Mascara = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TipoPessoa = table.Column<int>(type: "int", nullable: false),
                    Registro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizacoes_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parceiros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Fantasia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipodePessoa = table.Column<int>(type: "int", nullable: false),
                    Registro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parceiros", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_Parceiros_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrincipiosAtivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipiosAtivos", x => new { x.idconta, x.Id });
                    table.ForeignKey(
                        name: "FK_PrincipiosAtivos_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioConta",
                columns: table => new
                {
                    uid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    contaguid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioConta", x => new { x.uid, x.contaguid, x.idconta });
                    table.ForeignKey(
                        name: "FK_UsuarioConta_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelosMaquinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMarca = table.Column<int>(type: "int", nullable: false),
                    Combustivel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelosMaquinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelosMaquinas_MarcasMaquinas_IdMarca",
                        column: x => x.IdMarca,
                        principalTable: "MarcasMaquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CotacoesMoeda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMoeda = table.Column<int>(type: "int", nullable: false),
                    CotacaoData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CotacaoValor = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotacoesMoeda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CotacoesMoeda_Moedas_IdMoeda",
                        column: x => x.IdMoeda,
                        principalTable: "Moedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variedades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdCultura = table.Column<int>(type: "int", nullable: false),
                    Ciclo = table.Column<int>(type: "int", nullable: false),
                    CodigoExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTecnologia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variedades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variedades_Culturas_IdCultura",
                        column: x => x.IdCultura,
                        principalTable: "Culturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Variedades_Tecnologias_IdTecnologia",
                        column: x => x.IdTecnologia,
                        principalTable: "Tecnologias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoOperacao = table.Column<int>(type: "int", nullable: false),
                    Insumo = table.Column<bool>(type: "bit", nullable: false),
                    CodigoExterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rendimento = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Consumo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacoes", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_Operacoes_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operacoes_TiposOperacoes_IdTipoOperacao",
                        column: x => x.IdTipoOperacao,
                        principalTable: "TiposOperacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CodigoIBGE = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    IdUF = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipios_UFs_IdUF",
                        column: x => x.IdUF,
                        principalTable: "UFs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnosAgricolas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdOrganizacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnosAgricolas", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_AnosAgricolas_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnosAgricolas_Organizacoes_IdOrganizacao",
                        column: x => x.IdOrganizacao,
                        principalTable: "Organizacoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GruposContas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdOrganizacao = table.Column<int>(type: "int", nullable: false),
                    IdClasseConta = table.Column<int>(type: "int", nullable: false),
                    CodigoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoExterno = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposContas", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_GruposContas_ClassesContas_IdClasseConta",
                        column: x => x.IdClasseConta,
                        principalTable: "ClassesContas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GruposContas_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GruposContas_Organizacoes_IdOrganizacao",
                        column: x => x.IdOrganizacao,
                        principalTable: "Organizacoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizacaoUsuario",
                columns: table => new
                {
                    uid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    idorganizacao = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizacaoUsuario", x => new { x.uid, x.idorganizacao });
                    table.ForeignKey(
                        name: "FK_OrganizacaoUsuario_Organizacoes_idorganizacao",
                        column: x => x.idorganizacao,
                        principalTable: "Organizacoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdGrupoProduto = table.Column<int>(type: "int", nullable: false),
                    IdFabricante = table.Column<int>(type: "int", nullable: false),
                    unidadeBasica = table.Column<int>(type: "int", nullable: false),
                    IdPrincipioAtivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_Produtos_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produtos_GruposProdutos_IdGrupoProduto",
                        column: x => x.IdGrupoProduto,
                        principalTable: "GruposProdutos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produtos_Parceiros_IdFabricante_idconta",
                        columns: x => new { x.IdFabricante, x.idconta },
                        principalTable: "Parceiros",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produtos_PrincipiosAtivos_idconta_IdPrincipioAtivo",
                        columns: x => new { x.idconta, x.IdPrincipioAtivo },
                        principalTable: "PrincipiosAtivos",
                        principalColumns: new[] { "idconta", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maquinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    CodigoExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdModeloMaquina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquinas", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_Maquinas_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Maquinas_ModelosMaquinas_IdModeloMaquina",
                        column: x => x.IdModeloMaquina,
                        principalTable: "ModelosMaquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelosParametros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdModeloMaquina = table.Column<int>(type: "int", nullable: false),
                    IdCultura = table.Column<int>(type: "int", nullable: false),
                    IdOperacao = table.Column<int>(type: "int", nullable: false),
                    Rendimento = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Consumo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelosParametros", x => new { x.idconta, x.Id });
                    table.ForeignKey(
                        name: "FK_ModelosParametros_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModelosParametros_Culturas_IdCultura",
                        column: x => x.IdCultura,
                        principalTable: "Culturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelosParametros_ModelosMaquinas_IdModeloMaquina",
                        column: x => x.IdModeloMaquina,
                        principalTable: "ModelosMaquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelosParametros_Operacoes_IdOperacao_idconta",
                        columns: x => new { x.IdOperacao, x.idconta },
                        principalTable: "Operacoes",
                        principalColumns: new[] { "Id", "idconta" });
                });

            migrationBuilder.CreateTable(
                name: "Fazendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdOrganizacao = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdUF = table.Column<int>(type: "int", nullable: false),
                    IdMunicipio = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    TipoArrenda = table.Column<int>(type: "int", nullable: false),
                    ValorArrendamento = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CodigoExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdRegiao = table.Column<int>(type: "int", nullable: false),
                    IdCultura = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fazendas", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_Fazendas_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fazendas_Culturas_IdCultura",
                        column: x => x.IdCultura,
                        principalTable: "Culturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fazendas_Municipios_IdMunicipio",
                        column: x => x.IdMunicipio,
                        principalTable: "Municipios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fazendas_Organizacoes_IdOrganizacao",
                        column: x => x.IdOrganizacao,
                        principalTable: "Organizacoes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fazendas_Regioes_IdRegiao",
                        column: x => x.IdRegiao,
                        principalTable: "Regioes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fazendas_UFs_IdUF",
                        column: x => x.IdUF,
                        principalTable: "UFs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreferUsu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idorganizacao = table.Column<int>(type: "int", nullable: false),
                    idanoagricola = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferUsu", x => new { x.idconta, x.Id });
                    table.ForeignKey(
                        name: "FK_PreferUsu_AnosAgricolas_idanoagricola_idconta",
                        columns: x => new { x.idanoagricola, x.idconta },
                        principalTable: "AnosAgricolas",
                        principalColumns: new[] { "Id", "idconta" });
                    table.ForeignKey(
                        name: "FK_PreferUsu_Organizacoes_idorganizacao",
                        column: x => x.idorganizacao,
                        principalTable: "Organizacoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Safras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abertura = table.Column<bool>(type: "bit", nullable: false),
                    Reforma = table.Column<bool>(type: "bit", nullable: false),
                    IdCultura = table.Column<int>(type: "int", nullable: true),
                    IdAnoAgricola = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Safras", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_Safras_AnosAgricolas_IdAnoAgricola_idconta",
                        columns: x => new { x.IdAnoAgricola, x.idconta },
                        principalTable: "AnosAgricolas",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Safras_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Safras_Culturas_IdCultura",
                        column: x => x.IdCultura,
                        principalTable: "Culturas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CadastroContas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdGrupoConta = table.Column<int>(type: "int", nullable: false),
                    CodigoExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadastroContas", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_CadastroContas_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CadastroContas_GruposContas_IdGrupoConta_idconta",
                        columns: x => new { x.IdGrupoConta, x.idconta },
                        principalTable: "GruposContas",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaquinasParametros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdMaquina = table.Column<int>(type: "int", nullable: false),
                    IdCultura = table.Column<int>(type: "int", nullable: false),
                    IdOperacao = table.Column<int>(type: "int", nullable: false),
                    Rendimento = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Consumo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaquinasParametros", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_MaquinasParametros_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaquinasParametros_Culturas_IdCultura",
                        column: x => x.IdCultura,
                        principalTable: "Culturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaquinasParametros_Maquinas_IdMaquina_idconta",
                        columns: x => new { x.IdMaquina, x.idconta },
                        principalTable: "Maquinas",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaquinasParametros_Operacoes_IdOperacao_idconta",
                        columns: x => new { x.IdOperacao, x.idconta },
                        principalTable: "Operacoes",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Talhoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AreaProdutiva = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TipoArea = table.Column<int>(type: "int", nullable: false),
                    CodigoExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdFazenda = table.Column<int>(type: "int", nullable: false),
                    IdAnoAgricola = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talhoes", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_Talhoes_AnosAgricolas_IdAnoAgricola_idconta",
                        columns: x => new { x.IdAnoAgricola, x.idconta },
                        principalTable: "AnosAgricolas",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Talhoes_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Talhoes_Fazendas_IdFazenda_idconta",
                        columns: x => new { x.IdFazenda, x.idconta },
                        principalTable: "Fazendas",
                        principalColumns: new[] { "Id", "idconta" });
                });

            migrationBuilder.CreateTable(
                name: "Comercializacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdSafra = table.Column<int>(type: "int", nullable: false),
                    IdParceiro = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdMoeda = table.Column<int>(type: "int", nullable: false),
                    Trava = table.Column<bool>(type: "bit", nullable: false),
                    CBOT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cambio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Premio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descontos = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorLiquido = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NumeroContrato = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comercializacoes", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_Comercializacoes_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comercializacoes_Moedas_IdMoeda",
                        column: x => x.IdMoeda,
                        principalTable: "Moedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comercializacoes_Parceiros_IdParceiro_idconta",
                        columns: x => new { x.IdParceiro, x.idconta },
                        principalTable: "Parceiros",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comercializacoes_Safras_IdSafra_idconta",
                        columns: x => new { x.IdSafra, x.idconta },
                        principalTable: "Safras",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrcamentoProdutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSafra = table.Column<int>(type: "int", nullable: false),
                    IdFazenda = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrcamentoProdutos", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_OrcamentoProdutos_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrcamentoProdutos_Fazendas_IdFazenda_idconta",
                        columns: x => new { x.IdFazenda, x.idconta },
                        principalTable: "Fazendas",
                        principalColumns: new[] { "Id", "idconta" });
                    table.ForeignKey(
                        name: "FK_OrcamentoProdutos_Safras_IdSafra_idconta",
                        columns: x => new { x.IdSafra, x.idconta },
                        principalTable: "Safras",
                        principalColumns: new[] { "Id", "idconta" });
                });

            migrationBuilder.CreateTable(
                name: "PlanejamentoCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdSafra = table.Column<int>(type: "int", nullable: false),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    QtdNecessaria = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    QtdEstoque = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    QtdComprar = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    QtdComprada = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanejamentoCompras", x => new { x.idconta, x.Id });
                    table.ForeignKey(
                        name: "FK_PlanejamentoCompras_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanejamentoCompras_Produtos_IdProduto_idconta",
                        columns: x => new { x.IdProduto, x.idconta },
                        principalTable: "Produtos",
                        principalColumns: new[] { "Id", "idconta" });
                    table.ForeignKey(
                        name: "FK_PlanejamentoCompras_Safras_IdSafra_idconta",
                        columns: x => new { x.IdSafra, x.idconta },
                        principalTable: "Safras",
                        principalColumns: new[] { "Id", "idconta" });
                });

            migrationBuilder.CreateTable(
                name: "orcamentocustosindiretos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSafra = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idcontaCad = table.Column<int>(type: "int", nullable: false),
                    valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orcamentocustosindiretos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orcamentocustosindiretos_CadastroContas_idcontaCad_idconta",
                        columns: x => new { x.idcontaCad, x.idconta },
                        principalTable: "CadastroContas",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orcamentocustosindiretos_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_orcamentocustosindiretos_Safras_IdSafra_idconta",
                        columns: x => new { x.IdSafra, x.idconta },
                        principalTable: "Safras",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdSafra = table.Column<int>(type: "int", nullable: false),
                    IdTalhao = table.Column<int>(type: "int", nullable: false),
                    IdVariedade = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PopulacaoRecomendada = table.Column<int>(type: "int", nullable: false),
                    Germinacao = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    PMS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Espacamento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MargemSeguranca = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Stand = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnidadeSementePrevista = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtdSementePrevista = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProdEstimada = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigAreas", x => new { x.idconta, x.Id });
                    table.ForeignKey(
                        name: "FK_ConfigAreas_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigAreas_Safras_IdSafra_idconta",
                        columns: x => new { x.IdSafra, x.idconta },
                        principalTable: "Safras",
                        principalColumns: new[] { "Id", "idconta" });
                    table.ForeignKey(
                        name: "FK_ConfigAreas_Talhoes_IdTalhao_idconta",
                        columns: x => new { x.IdTalhao, x.idconta },
                        principalTable: "Talhoes",
                        principalColumns: new[] { "Id", "idconta" });
                    table.ForeignKey(
                        name: "FK_ConfigAreas_Variedades_IdVariedade",
                        column: x => x.IdVariedade,
                        principalTable: "Variedades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "entregaContratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdComercializacao = table.Column<int>(type: "int", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entregaContratos", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_entregaContratos_Comercializacoes_IdComercializacao_idconta",
                        columns: x => new { x.IdComercializacao, x.idconta },
                        principalTable: "Comercializacoes",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entregaContratos_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProdutosOrcamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdOrcamento = table.Column<int>(type: "int", nullable: false),
                    TipoProdutoOrc = table.Column<int>(type: "int", nullable: false),
                    IdPrincipioAtivo = table.Column<int>(type: "int", nullable: true),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    DataPreco = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosOrcamento", x => new { x.idconta, x.Id });
                    table.ForeignKey(
                        name: "FK_ProdutosOrcamento_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutosOrcamento_OrcamentoProdutos_IdOrcamento_idconta",
                        columns: x => new { x.IdOrcamento, x.idconta },
                        principalTable: "OrcamentoProdutos",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutosOrcamento_PrincipiosAtivos_idconta_IdPrincipioAtivo",
                        columns: x => new { x.idconta, x.IdPrincipioAtivo },
                        principalTable: "PrincipiosAtivos",
                        principalColumns: new[] { "idconta", "Id" });
                    table.ForeignKey(
                        name: "FK_ProdutosOrcamento_Produtos_IdProduto_idconta",
                        columns: x => new { x.IdProduto, x.idconta },
                        principalTable: "Produtos",
                        principalColumns: new[] { "Id", "idconta" });
                });

            migrationBuilder.CreateTable(
                name: "PlanejamentoOperacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdConfigArea = table.Column<int>(type: "int", nullable: false),
                    DataPrevista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdOperacao = table.Column<int>(type: "int", nullable: false),
                    Plantio = table.Column<bool>(type: "bit", nullable: false),
                    DAE = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    QHorasEstimadas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    QCombustivelEstimado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", precision: 18, scale: 2, nullable: false),
                    CustoOperacao = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanejamentoOperacoes", x => new { x.Id, x.idconta });
                    table.ForeignKey(
                        name: "FK_PlanejamentoOperacoes_ConfigAreas_idconta_IdConfigArea",
                        columns: x => new { x.idconta, x.IdConfigArea },
                        principalTable: "ConfigAreas",
                        principalColumns: new[] { "idconta", "Id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanejamentoOperacoes_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanejamentoOperacoes_Operacoes_IdOperacao_idconta",
                        columns: x => new { x.IdOperacao, x.idconta },
                        principalTable: "Operacoes",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaquinasPlanejadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdPlanejamento = table.Column<int>(type: "int", nullable: false),
                    IdModeloMaquina = table.Column<int>(type: "int", nullable: false),
                    IdMaquina = table.Column<int>(type: "int", nullable: false),
                    Rendimento = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Consumo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    QtdHoraEstimada = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    QtdCombEstimado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaquinasPlanejadas", x => new { x.idconta, x.Id });
                    table.ForeignKey(
                        name: "FK_MaquinasPlanejadas_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaquinasPlanejadas_Maquinas_IdMaquina_idconta",
                        columns: x => new { x.IdMaquina, x.idconta },
                        principalTable: "Maquinas",
                        principalColumns: new[] { "Id", "idconta" });
                    table.ForeignKey(
                        name: "FK_MaquinasPlanejadas_ModelosMaquinas_IdModeloMaquina",
                        column: x => x.IdModeloMaquina,
                        principalTable: "ModelosMaquinas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaquinasPlanejadas_PlanejamentoOperacoes_IdPlanejamento_idconta",
                        columns: x => new { x.IdPlanejamento, x.idconta },
                        principalTable: "PlanejamentoOperacoes",
                        principalColumns: new[] { "Id", "idconta" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutosPlanejados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idconta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tamanho = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AreaPercent = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    IdPlanejamento = table.Column<int>(type: "int", nullable: false),
                    IdPrincipioAtivo = table.Column<int>(type: "int", nullable: true),
                    IdProduto = table.Column<int>(type: "int", nullable: true),
                    Dosagem = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalProduto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosPlanejados", x => new { x.idconta, x.Id });
                    table.ForeignKey(
                        name: "FK_ProdutosPlanejados_Contas_idconta",
                        column: x => x.idconta,
                        principalTable: "Contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutosPlanejados_PlanejamentoOperacoes_IdPlanejamento_idconta",
                        columns: x => new { x.IdPlanejamento, x.idconta },
                        principalTable: "PlanejamentoOperacoes",
                        principalColumns: new[] { "Id", "idconta" });
                    table.ForeignKey(
                        name: "FK_ProdutosPlanejados_PrincipiosAtivos_idconta_IdPrincipioAtivo",
                        columns: x => new { x.idconta, x.IdPrincipioAtivo },
                        principalTable: "PrincipiosAtivos",
                        principalColumns: new[] { "idconta", "Id" });
                    table.ForeignKey(
                        name: "FK_ProdutosPlanejados_Produtos_IdProduto_idconta",
                        columns: x => new { x.IdProduto, x.idconta },
                        principalTable: "Produtos",
                        principalColumns: new[] { "Id", "idconta" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnosAgricolas_idconta",
                table: "AnosAgricolas",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_AnosAgricolas_IdOrganizacao",
                table: "AnosAgricolas",
                column: "IdOrganizacao");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturaConta_idconta",
                table: "AssinaturaConta",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_CadastroContas_idconta",
                table: "CadastroContas",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_CadastroContas_IdGrupoConta_idconta",
                table: "CadastroContas",
                columns: new[] { "IdGrupoConta", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_Comercializacoes_idconta",
                table: "Comercializacoes",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_Comercializacoes_IdMoeda",
                table: "Comercializacoes",
                column: "IdMoeda");

            migrationBuilder.CreateIndex(
                name: "IX_Comercializacoes_IdParceiro_idconta",
                table: "Comercializacoes",
                columns: new[] { "IdParceiro", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_Comercializacoes_IdSafra_idconta",
                table: "Comercializacoes",
                columns: new[] { "IdSafra", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigAreas_IdSafra_idconta",
                table: "ConfigAreas",
                columns: new[] { "IdSafra", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigAreas_IdTalhao_idconta",
                table: "ConfigAreas",
                columns: new[] { "IdTalhao", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigAreas_IdVariedade",
                table: "ConfigAreas",
                column: "IdVariedade");

            migrationBuilder.CreateIndex(
                name: "IX_CotacoesMoeda_IdMoeda",
                table: "CotacoesMoeda",
                column: "IdMoeda");

            migrationBuilder.CreateIndex(
                name: "IX_entregaContratos_IdComercializacao_idconta",
                table: "entregaContratos",
                columns: new[] { "IdComercializacao", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_entregaContratos_idconta",
                table: "entregaContratos",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_Fazendas_idconta",
                table: "Fazendas",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_Fazendas_IdCultura",
                table: "Fazendas",
                column: "IdCultura");

            migrationBuilder.CreateIndex(
                name: "IX_Fazendas_IdMunicipio",
                table: "Fazendas",
                column: "IdMunicipio");

            migrationBuilder.CreateIndex(
                name: "IX_Fazendas_IdOrganizacao",
                table: "Fazendas",
                column: "IdOrganizacao");

            migrationBuilder.CreateIndex(
                name: "IX_Fazendas_IdRegiao",
                table: "Fazendas",
                column: "IdRegiao");

            migrationBuilder.CreateIndex(
                name: "IX_Fazendas_IdUF",
                table: "Fazendas",
                column: "IdUF");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceiroConta_idconta",
                table: "FinanceiroConta",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_GruposContas_IdClasseConta",
                table: "GruposContas",
                column: "IdClasseConta");

            migrationBuilder.CreateIndex(
                name: "IX_GruposContas_idconta",
                table: "GruposContas",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_GruposContas_IdOrganizacao",
                table: "GruposContas",
                column: "IdOrganizacao");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_idconta",
                table: "Maquinas",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_IdModeloMaquina",
                table: "Maquinas",
                column: "IdModeloMaquina");

            migrationBuilder.CreateIndex(
                name: "IX_MaquinasParametros_idconta",
                table: "MaquinasParametros",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_MaquinasParametros_IdCultura",
                table: "MaquinasParametros",
                column: "IdCultura");

            migrationBuilder.CreateIndex(
                name: "IX_MaquinasParametros_IdMaquina_idconta",
                table: "MaquinasParametros",
                columns: new[] { "IdMaquina", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_MaquinasParametros_IdOperacao_idconta",
                table: "MaquinasParametros",
                columns: new[] { "IdOperacao", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_MaquinasPlanejadas_IdMaquina_idconta",
                table: "MaquinasPlanejadas",
                columns: new[] { "IdMaquina", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_MaquinasPlanejadas_IdModeloMaquina",
                table: "MaquinasPlanejadas",
                column: "IdModeloMaquina");

            migrationBuilder.CreateIndex(
                name: "IX_MaquinasPlanejadas_IdPlanejamento_idconta",
                table: "MaquinasPlanejadas",
                columns: new[] { "IdPlanejamento", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_ModelosMaquinas_IdMarca",
                table: "ModelosMaquinas",
                column: "IdMarca");

            migrationBuilder.CreateIndex(
                name: "IX_ModelosParametros_IdCultura",
                table: "ModelosParametros",
                column: "IdCultura");

            migrationBuilder.CreateIndex(
                name: "IX_ModelosParametros_IdModeloMaquina",
                table: "ModelosParametros",
                column: "IdModeloMaquina");

            migrationBuilder.CreateIndex(
                name: "IX_ModelosParametros_IdOperacao_idconta",
                table: "ModelosParametros",
                columns: new[] { "IdOperacao", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_IdUF",
                table: "Municipios",
                column: "IdUF");

            migrationBuilder.CreateIndex(
                name: "IX_Operacoes_idconta",
                table: "Operacoes",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_Operacoes_IdTipoOperacao",
                table: "Operacoes",
                column: "IdTipoOperacao");

            migrationBuilder.CreateIndex(
                name: "IX_orcamentocustosindiretos_idconta",
                table: "orcamentocustosindiretos",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_orcamentocustosindiretos_idcontaCad_idconta",
                table: "orcamentocustosindiretos",
                columns: new[] { "idcontaCad", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_orcamentocustosindiretos_IdSafra_idconta",
                table: "orcamentocustosindiretos",
                columns: new[] { "IdSafra", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentoProdutos_idconta",
                table: "OrcamentoProdutos",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentoProdutos_IdFazenda_idconta",
                table: "OrcamentoProdutos",
                columns: new[] { "IdFazenda", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentoProdutos_IdSafra_idconta",
                table: "OrcamentoProdutos",
                columns: new[] { "IdSafra", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizacaoUsuario_idorganizacao",
                table: "OrganizacaoUsuario",
                column: "idorganizacao");

            migrationBuilder.CreateIndex(
                name: "IX_Organizacoes_idconta",
                table: "Organizacoes",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiros_idconta",
                table: "Parceiros",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoCompras_IdProduto_idconta",
                table: "PlanejamentoCompras",
                columns: new[] { "IdProduto", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoCompras_IdSafra_idconta",
                table: "PlanejamentoCompras",
                columns: new[] { "IdSafra", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoOperacoes_idconta_IdConfigArea",
                table: "PlanejamentoOperacoes",
                columns: new[] { "idconta", "IdConfigArea" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoOperacoes_IdOperacao_idconta",
                table: "PlanejamentoOperacoes",
                columns: new[] { "IdOperacao", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_PreferUsu_idanoagricola_idconta",
                table: "PreferUsu",
                columns: new[] { "idanoagricola", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_PreferUsu_idorganizacao",
                table: "PreferUsu",
                column: "idorganizacao");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_idconta_IdPrincipioAtivo",
                table: "Produtos",
                columns: new[] { "idconta", "IdPrincipioAtivo" });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_IdFabricante_idconta",
                table: "Produtos",
                columns: new[] { "IdFabricante", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_IdGrupoProduto",
                table: "Produtos",
                column: "IdGrupoProduto");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosOrcamento_idconta_IdPrincipioAtivo",
                table: "ProdutosOrcamento",
                columns: new[] { "idconta", "IdPrincipioAtivo" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosOrcamento_IdOrcamento_idconta",
                table: "ProdutosOrcamento",
                columns: new[] { "IdOrcamento", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosOrcamento_IdProduto_idconta",
                table: "ProdutosOrcamento",
                columns: new[] { "IdProduto", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosPlanejados_idconta_IdPrincipioAtivo",
                table: "ProdutosPlanejados",
                columns: new[] { "idconta", "IdPrincipioAtivo" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosPlanejados_IdPlanejamento_idconta",
                table: "ProdutosPlanejados",
                columns: new[] { "IdPlanejamento", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosPlanejados_IdProduto_idconta",
                table: "ProdutosPlanejados",
                columns: new[] { "IdProduto", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_Safras_IdAnoAgricola_idconta",
                table: "Safras",
                columns: new[] { "IdAnoAgricola", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_Safras_idconta",
                table: "Safras",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_Safras_IdCultura",
                table: "Safras",
                column: "IdCultura");

            migrationBuilder.CreateIndex(
                name: "IX_Talhoes_IdAnoAgricola_idconta",
                table: "Talhoes",
                columns: new[] { "IdAnoAgricola", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_Talhoes_idconta",
                table: "Talhoes",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_Talhoes_IdFazenda_idconta",
                table: "Talhoes",
                columns: new[] { "IdFazenda", "idconta" });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioConta_idconta",
                table: "UsuarioConta",
                column: "idconta");

            migrationBuilder.CreateIndex(
                name: "IX_Variedades_IdCultura",
                table: "Variedades",
                column: "IdCultura");

            migrationBuilder.CreateIndex(
                name: "IX_Variedades_IdTecnologia",
                table: "Variedades",
                column: "IdTecnologia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssinaturaConta");

            migrationBuilder.DropTable(
                name: "CotacoesMoeda");

            migrationBuilder.DropTable(
                name: "entregaContratos");

            migrationBuilder.DropTable(
                name: "FinanceiroConta");

            migrationBuilder.DropTable(
                name: "MaquinasParametros");

            migrationBuilder.DropTable(
                name: "MaquinasPlanejadas");

            migrationBuilder.DropTable(
                name: "ModelosParametros");

            migrationBuilder.DropTable(
                name: "orcamentocustosindiretos");

            migrationBuilder.DropTable(
                name: "OrganizacaoUsuario");

            migrationBuilder.DropTable(
                name: "PlanejamentoCompras");

            migrationBuilder.DropTable(
                name: "PreferUsu");

            migrationBuilder.DropTable(
                name: "ProdutosOrcamento");

            migrationBuilder.DropTable(
                name: "ProdutosPlanejados");

            migrationBuilder.DropTable(
                name: "UsuarioConta");

            migrationBuilder.DropTable(
                name: "Comercializacoes");

            migrationBuilder.DropTable(
                name: "Maquinas");

            migrationBuilder.DropTable(
                name: "CadastroContas");

            migrationBuilder.DropTable(
                name: "OrcamentoProdutos");

            migrationBuilder.DropTable(
                name: "PlanejamentoOperacoes");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Moedas");

            migrationBuilder.DropTable(
                name: "ModelosMaquinas");

            migrationBuilder.DropTable(
                name: "GruposContas");

            migrationBuilder.DropTable(
                name: "ConfigAreas");

            migrationBuilder.DropTable(
                name: "Operacoes");

            migrationBuilder.DropTable(
                name: "GruposProdutos");

            migrationBuilder.DropTable(
                name: "Parceiros");

            migrationBuilder.DropTable(
                name: "PrincipiosAtivos");

            migrationBuilder.DropTable(
                name: "MarcasMaquinas");

            migrationBuilder.DropTable(
                name: "ClassesContas");

            migrationBuilder.DropTable(
                name: "Safras");

            migrationBuilder.DropTable(
                name: "Talhoes");

            migrationBuilder.DropTable(
                name: "Variedades");

            migrationBuilder.DropTable(
                name: "TiposOperacoes");

            migrationBuilder.DropTable(
                name: "AnosAgricolas");

            migrationBuilder.DropTable(
                name: "Fazendas");

            migrationBuilder.DropTable(
                name: "Tecnologias");

            migrationBuilder.DropTable(
                name: "Culturas");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "Organizacoes");

            migrationBuilder.DropTable(
                name: "Regioes");

            migrationBuilder.DropTable(
                name: "UFs");

            migrationBuilder.DropTable(
                name: "Contas");
        }
    }
}
