using FarmPlannerAPI.Entities;
using FarmPlannerAPI.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FarmPlannerAPI.Context
{
    public class FarmPlannerContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public FarmPlannerContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Conta> contas => Set<Conta>();
        public DbSet<UsuarioConta> usuarioscontas => Set<UsuarioConta>();
        public DbSet<Organizacao> organizacoes => Set<Organizacao>();
        public DbSet<AnoAgricola> anosagricolas => Set<AnoAgricola>();
        public DbSet<Cultura> culturas => Set<Cultura>();
        public DbSet<Safra> safras => Set<Safra>();
        public DbSet<Fazenda> fazendas => Set<Fazenda>();
        public DbSet<Talhao> talhoes => Set<Talhao>();
        public DbSet<Variedade> variedades => Set<Variedade>();
        public DbSet<UF> ufs => Set<UF>();
        public DbSet<Municipio> municipios => Set<Municipio>();
        public DbSet<Tecnologia> tecnologias => Set<Tecnologia>();
        public DbSet<PrincipioAtivo> principioAtivos => Set<PrincipioAtivo>();
        public DbSet<GrupoProduto> gruposprodutos => Set<GrupoProduto>();

        public DbSet<Produto> produtos => Set<Produto>();
        public DbSet<TipoOperacao> tiposoperacao => Set<TipoOperacao>();

        public DbSet<Operacao> operacoes => Set<Operacao>();
        public DbSet<Parceiro> parceiros => Set<Parceiro>();

        public DbSet<MarcaMaquina> marcasmaquinas => Set<MarcaMaquina>();

        public DbSet<ModeloMaquina> modelosmaquinas => Set<ModeloMaquina>();
        public DbSet<ModeloParametro> modelosparametros => Set<ModeloParametro>();

        public DbSet<Maquina> maquinas => Set<Maquina>();
        public DbSet<MaquinaParametro> maquinasparametro => Set<MaquinaParametro>();
        public DbSet<Regiao> regioes => Set<Regiao>();

        public DbSet<ConfigArea> configareas => Set<ConfigArea>();
        public DbSet<OrcamentoProduto> orcamentosproduto => Set<OrcamentoProduto>();
        public DbSet<ProdutoOrcamento> produtosorcamento => Set<ProdutoOrcamento>();
        public DbSet<PlanejamentoOperacao> planejoperacoes => Set<PlanejamentoOperacao>();
        public DbSet<MaquinaPlanejada> maquinasplanejadas => Set<MaquinaPlanejada>();
        public DbSet<ProdutoPlanejado> produtoplanejados => Set<ProdutoPlanejado>();
        public DbSet<Moeda> moedas => Set<Moeda>();
        public DbSet<CotacaoMoeda> cotacoesmoedas => Set<CotacaoMoeda>();
        public DbSet<Comercializacao> comercializacoes => Set<Comercializacao>();
        public DbSet<PlanejamentoCompra> planejamentoCompras => Set<PlanejamentoCompra>();
        public DbSet<ClasseConta> classescontas => Set<ClasseConta>();
        public DbSet<GrupoConta> gruposcontas => Set<GrupoConta>();
        public DbSet<OrcamentoCustoIndireto> orcamentocustosindiretos => Set<OrcamentoCustoIndireto>();
        public DbSet<CadastroConta> cadastrocontas => Set<CadastroConta>();
        public DbSet<EntregaContrato> entregaContratos => Set<EntregaContrato>();
        public DbSet<FinanceiroConta> financeiroContas => Set<FinanceiroConta>();
        public DbSet<AssinaturaConta> assinaturaContas => Set<AssinaturaConta>();
        public DbSet<OrganizacaoUsuario> organizacaoUsuarios => Set<OrganizacaoUsuario>();
        public DbSet<Unidade> unidades => Set<Unidade>();
        public DbSet<FarmPlannerLog> farmPlannerLogs => Set<FarmPlannerLog>();

        public DbSet<ProdutoPrincipioAtivo> produtosprincipio => Set<ProdutoPrincipioAtivo>();

        public DbSet<PreferUsu> preferUsus => Set<PreferUsu>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("FarmPlanner"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizacaoConfiguration());
            modelBuilder.ApplyConfiguration(new CulturaConfiguration());
            modelBuilder.ApplyConfiguration(new TecnologiaConfiguration());
            modelBuilder.ApplyConfiguration(new VariedadeConfiguration());
            modelBuilder.ApplyConfiguration(new UFConfiguration());
            modelBuilder.ApplyConfiguration(new MunicipioConfiguration());
            modelBuilder.ApplyConfiguration(new RegiaoConfiguration());
            modelBuilder.ApplyConfiguration(new SafraConfiguration());
            modelBuilder.ApplyConfiguration(new FazendaConfiguration());
            modelBuilder.ApplyConfiguration(new TalhaoConfiguration());
            modelBuilder.ApplyConfiguration(new AnoAgricolaConfiguration());
            modelBuilder.ApplyConfiguration(new ContaConfiguration());
            modelBuilder.ApplyConfiguration(new GrupoProdutoConfigurations());
            modelBuilder.ApplyConfiguration(new MaquinaConfiguration());
            modelBuilder.ApplyConfiguration(new MaquinaParametroConfiguration());
            modelBuilder.ApplyConfiguration(new MarcaMaquinaConfiguration());
            modelBuilder.ApplyConfiguration(new ModeloMaquinaConfiguration());
            modelBuilder.ApplyConfiguration(new ModeloParametroConfiguration());
            modelBuilder.ApplyConfiguration(new OperacaoConfiguration());
            modelBuilder.ApplyConfiguration(new ParceiroConfiguration());
            modelBuilder.ApplyConfiguration(new PrincipioAtivoConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new SafraConfiguration());
            modelBuilder.ApplyConfiguration(new TalhaoConfiguration());
            modelBuilder.ApplyConfiguration(new TipoOperacaoConfiguration());
            modelBuilder.ApplyConfiguration(new MaquinaConfiguration());
            modelBuilder.ApplyConfiguration(new ConfigAreaConfiguration());
            modelBuilder.ApplyConfiguration(new OrcamentoProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoOrcamentoConfiguration());
            modelBuilder.ApplyConfiguration(new PlanejamentoOperacaoConfiguration());
            modelBuilder.ApplyConfiguration(new MaquinaPlanejadaConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoPlanejadoConfiguration());
            modelBuilder.ApplyConfiguration(new MoedaConfiguration());
            modelBuilder.ApplyConfiguration(new CotacaoMoedaConfiguration());
            modelBuilder.ApplyConfiguration(new ComercializacaoConfiguration());
            modelBuilder.ApplyConfiguration(new PlanejamentoCompraConfiguration());
            modelBuilder.ApplyConfiguration(new ClasseContaConfiguration());
            modelBuilder.ApplyConfiguration(new GrupoContaConfiguration());
            modelBuilder.ApplyConfiguration(new CadastroContaConfiguration());
            modelBuilder.ApplyConfiguration(new EntregaContratoConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioContaConfiguration());
            modelBuilder.ApplyConfiguration(new FinanceiroContaConfiguration());
            modelBuilder.ApplyConfiguration(new AssinaturaContaConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizacaoUsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new PreferUsuConfiguration());
            modelBuilder.ApplyConfiguration(new UnidadeConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoPrincipioConfiguration());
        }
    }
}