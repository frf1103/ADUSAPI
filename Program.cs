using FarmPlannerAPI.Context;
using FarmPlannerAPI.Middlewares;
using FarmPlannerAPI.Services;
using FarmPlannerAPI.Validators.AnoAgricola;
using FarmPlannerAPI.Validators.Comercializacao;
using FarmPlannerAPI.Validators.ConfigArea;
using FarmPlannerAPI.Validators.Conta;
using FarmPlannerAPI.Validators.Cultura;
using FarmPlannerAPI.Validators.CustosIndiretos;
using FarmPlannerAPI.Validators.Fazenda;
using FarmPlannerAPI.Validators.GrupoProduto;
using FarmPlannerAPI.Validators.Localidade;
using FarmPlannerAPI.Validators.MaquinaPlanejada;
using FarmPlannerAPI.Validators.Maquinas;
using FarmPlannerAPI.Validators.Moeda;
using FarmPlannerAPI.Validators.Operacao;
using FarmPlannerAPI.Validators.OrcamentoProduto;
using FarmPlannerAPI.Validators.Organizacao;
using FarmPlannerAPI.Validators.Parceiro;
using FarmPlannerAPI.Validators.PedidoCompra;
using FarmPlannerAPI.Validators.PlanejamentoCompra;
using FarmPlannerAPI.Validators.PlanejamentoOperacao;
using FarmPlannerAPI.Validators.PrincipioAtivo;
using FarmPlannerAPI.Validators.Produto;
using FarmPlannerAPI.Validators.ProdutoCompra;
using FarmPlannerAPI.Validators.ProdutoPlanejado;
using FarmPlannerAPI.Validators.Safra;
using FarmPlannerAPI.Validators.Talhao;
using FarmPlannerAPI.Validators.Tecnologia;
using FarmPlannerAPI.Validators.TipoOperacao;
using FarmPlannerAPI.Validators.Unidade;
using FarmPlannerAPI.Validators.Variedade;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using static FarmPlannerAPI.Validators.EntregaCompra.EntregaCompraValidator;
using static FarmPlannerAPI.Validators.Variedade.VariedadeValidator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FarmPlannerContext>();
builder.Services.AddTransient<ContaService>();
builder.Services.AddTransient<AdicionarContaValidator>();
builder.Services.AddTransient<OrganizacaoService>();
builder.Services.AddTransient<AdicionarOrganizacaoValidator>();
builder.Services.AddTransient<EditarOrganizacaoValidator>();
builder.Services.AddTransient<AnoAgricolaService>();
builder.Services.AddTransient<AdicionarAnoAgricolaValidator>();
builder.Services.AddTransient<ExcluirAnoAgricolaValidator>();
builder.Services.AddTransient<CulturaService>();
builder.Services.AddTransient<CulturaValidator>();
builder.Services.AddTransient<ExcluirCulturaValidator>();
builder.Services.AddTransient<VariedadeService>();
builder.Services.AddTransient<VariedadeValidator>();
builder.Services.AddTransient<ExcluirVariedadeValidator>();
builder.Services.AddTransient<TecnologiaService>();
builder.Services.AddTransient<TecnologiaValidator>();
builder.Services.AddTransient<ExcluirTecnologiaValidator>();
builder.Services.AddTransient<LocalidadeService>();
builder.Services.AddTransient<ExcluirRegiaoValidator>();
builder.Services.AddTransient<RegiaoValidator>();
builder.Services.AddTransient<RegiaoService>();
builder.Services.AddTransient<ExcluirFazendaValidator>();
builder.Services.AddTransient<FazendaValidator>();
builder.Services.AddTransient<FazendaService>();
builder.Services.AddTransient<ExcluirTalhaoValidator>();
builder.Services.AddTransient<TalhaoValidator>();
builder.Services.AddTransient<TalhaoService>();
builder.Services.AddTransient<ExcluirPrincipioAtivoValidator>();
builder.Services.AddTransient<PrincipioAtivoValidator>();
builder.Services.AddTransient<PrincipioAtivoService>();
builder.Services.AddTransient<ExcluirGrupoProdutoValidator>();
builder.Services.AddTransient<GrupoProdutoValidator>();
builder.Services.AddTransient<GrupoProdutoService>();

builder.Services.AddTransient<ExcluirParceiroValidator>();
builder.Services.AddTransient<ParceiroValidator>();
builder.Services.AddTransient<ParceiroService>();

builder.Services.AddTransient<ExcluirProdutoValidator>();
builder.Services.AddTransient<ProdutoValidator>();
builder.Services.AddTransient<ProdutoService>();

builder.Services.AddTransient<ExcluirSafraValidator>();
builder.Services.AddTransient<SafraValidator>();
builder.Services.AddTransient<SafraService>();

builder.Services.AddTransient<ExcluirTipoOperacaoValidator>();
builder.Services.AddTransient<TipoOperacaoValidator>();
builder.Services.AddTransient<TipoOperacaoService>();

builder.Services.AddTransient<ExcluirOperacaoValidator>();
builder.Services.AddTransient<OperacaoValidator>();
builder.Services.AddTransient<OperacaoService>();

builder.Services.AddTransient<ExcluirMarcaMaquinaValidator>();
builder.Services.AddTransient<MarcaMaquinaValidator>();
builder.Services.AddTransient<MarcaMaquinaService>();

builder.Services.AddTransient<ExcluirModeloMaquinaValidator>();
builder.Services.AddTransient<ModeloMaquinaValidator>();
builder.Services.AddTransient<ModeloMaquinaService>();

builder.Services.AddTransient<ModeloParametroValidator>();
builder.Services.AddTransient<ModeloParametroService>();

builder.Services.AddTransient<ExcluirMaquinaValidator>();
builder.Services.AddTransient<MaquinaValidator>();
builder.Services.AddTransient<MaquinaService>();

builder.Services.AddTransient<MaquinaParametroValidator>();
builder.Services.AddTransient<MaquinaParametroService>();

builder.Services.AddTransient<EditarConfigAreaValidator>();
builder.Services.AddTransient<AdicionarConfigAreaValidator>();
builder.Services.AddTransient<ExcluirConfigAreaValidator>();
builder.Services.AddTransient<ConfigAreaService>();

builder.Services.AddTransient<ExcluirOrcamentoProdutoValidator>();
builder.Services.AddTransient<OrcamentoProdutoValidator>();
builder.Services.AddTransient<OrcamentoProdutoService>();

builder.Services.AddTransient<ExcluirProdutoOrcamentoValidator>();
builder.Services.AddTransient<ProdutoOrcamentoValidator>();
builder.Services.AddTransient<ProdutoOrcamentoService>();

builder.Services.AddTransient<ExcluirPlanejamentoOperacaoValidator>();
builder.Services.AddTransient<PlanejamentoOperacaoValidator>();
builder.Services.AddTransient<PlanejamentoOperacaoService>();

builder.Services.AddTransient<ExcluirMaquinaPlanejadaValidator>();
builder.Services.AddTransient<MaquinaPlanejadaValidator>();
builder.Services.AddTransient<MaquinaPlanejadaService>();

builder.Services.AddTransient<ProdutoPlanejadoValidator>();
builder.Services.AddTransient<ExcluirProdutoPlanejadoValidator>();
builder.Services.AddTransient<ProdutoPlanejadoService>();

builder.Services.AddTransient<MoedaValidator>();
builder.Services.AddTransient<ExcluirMoedaValidator>();
builder.Services.AddTransient<MoedaService>();

builder.Services.AddTransient<CotacaoMoedaValidator>();
builder.Services.AddTransient<ExcluirCotacaoMoedaValidator>();
builder.Services.AddTransient<CotacaoMoedaService>();

builder.Services.AddTransient<PlanejamentoCompraValidator>();
builder.Services.AddTransient<ExcluirPlanejamentoCompraValidator>();
builder.Services.AddTransient<PlanejamentoCompraService>();

builder.Services.AddTransient<OrcamentoCustoIndiretoValidator>();
builder.Services.AddTransient<ExcluirOrcamentoCustoIndiretoValidator>();
builder.Services.AddTransient<OrcamentoCustoIndiretoService>();

builder.Services.AddTransient<CadastroContaValidator>();
builder.Services.AddTransient<ExcluirCadastroContaValidator>();
builder.Services.AddTransient<CadastroContaService>();

builder.Services.AddTransient<GrupoContaValidator>();
builder.Services.AddTransient<ExcluirGrupoContaValidator>();
builder.Services.AddTransient<GrupoContaService>();

builder.Services.AddTransient<ClasseContaValidator>();
builder.Services.AddTransient<ExcluirClasseContaValidator>();
builder.Services.AddTransient<ClasseContaService>();

builder.Services.AddTransient<EntregaContratoValidator>();
builder.Services.AddTransient<ExcluirEntregaContratoValidator>();
builder.Services.AddTransient<EntregaContratoService>();

builder.Services.AddTransient<UnidadeService>();
builder.Services.AddTransient<UnidadeValidator>();
builder.Services.AddTransient<ExcluirUnidadeValidator>();

builder.Services.AddTransient<ProdutoPrincipioValidator>();
builder.Services.AddTransient<PreferUsuService>();

builder.Services.AddTransient<PedidoCompraService>();
builder.Services.AddTransient<PedidoCompraValidator>();
builder.Services.AddTransient<ExcluirPedidoCompraValidator>();

builder.Services.AddTransient<ProdutoCompraService>();
builder.Services.AddTransient<ProdutoCompraValidator>();
builder.Services.AddTransient<ExcluirProdutoCompraValidator>();

builder.Services.AddTransient<EntregaCompraService>();
builder.Services.AddTransient<AddEntregaCompraValidator>();
builder.Services.AddTransient<ExcluirEntregaCompraValidator>();

builder.Services.AddTransient<ComercializacaoService>();
builder.Services.AddTransient<ComercializacaoValidator>();
builder.Services.AddTransient<ExcluirComercializacaoValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Logging.AddEventLog();

var app = builder.Build();
app.UseCors("AllowAllOrigins");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FarmPlannerContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ValidationExceptionHandlerMiddleware>();

app.Run();