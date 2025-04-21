using ADUSAPI.Configurations;
using ADUSAPI.Entities;
using ADUSAPI.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.Context
{
    public class ADUSContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ADUSContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<UF> ufs => Set<UF>();
        public DbSet<Municipio> municipios => Set<Municipio>();
        public DbSet<Parceiro> parceiros => Set<Parceiro>();

        public DbSet<Transacao> transacoes => Set<Transacao>();
        public DbSet<Assinatura> assinaturas => Set<Assinatura>();

        public DbSet<PlanoConta> PlanoContas => Set<PlanoConta>();
        public DbSet<Moeda> moedas => Set<Moeda>();
        public DbSet<Banco> bancos => Set<Banco>();
        public DbSet<ContaCorrente> contascorrentes => Set<ContaCorrente>();
        public DbSet<CotacaoMoeda> cotacoesmoedas => Set<CotacaoMoeda>();
        public DbSet<ADUSLog> ADUSLogs => Set<ADUSLog>();

        public DbSet<ParametrosGuru> parametrosguru => Set<ParametrosGuru>();
        public DbSet<Parcela> parcelas => Set<Parcela>();

        public DbSet<MovimentoCaixa> movimentoCaixas => Set<MovimentoCaixa>();

        public DbSet<CentroCusto> centroCustos => Set<CentroCusto>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("ADUS"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UFConfiguration());
            modelBuilder.ApplyConfiguration(new MunicipioConfiguration());
            modelBuilder.ApplyConfiguration(new ParceiroConfiguration());
            modelBuilder.ApplyConfiguration(new MoedaConfiguration());
            modelBuilder.ApplyConfiguration(new CotacaoMoedaConfiguration());
            modelBuilder.ApplyConfiguration(new AssinaturaConfiguration());
            modelBuilder.ApplyConfiguration(new ParametrosGuruConfiguration());
            modelBuilder.ApplyConfiguration(new ParcelaConfiguration());
            modelBuilder.ApplyConfiguration(new BancoConfiguration());
            modelBuilder.ApplyConfiguration(new ContaCorrenteConfiguration());
            modelBuilder.ApplyConfiguration(new PlanoContaConfiguration());
            modelBuilder.ApplyConfiguration(new TransacaoConfiguration());
            modelBuilder.ApplyConfiguration(new CentroCustoConfiguration());
            modelBuilder.ApplyConfiguration(new MovimentoCaixaConfiguration());
        }
    }
}