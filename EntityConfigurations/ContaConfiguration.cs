using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Contas");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CPF).IsRequired(false).HasMaxLength(11);
            builder.Property(x => x.ContaGuid);
            builder.Property(x => x.uid);
            builder.Property(x => x.telefone);
            builder.Property(x => x.representanteid).IsRequired(false);
            builder.Property(x => x.ativa).IsRequired(false).HasDefaultValue(false);
            builder.HasMany(x => x.organizacoes).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.anoAgricolas).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.cadastroContas).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.comercializas).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.entregaContrato).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.fazendas).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.grupoContas).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.maquinas).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.maquinaParametros).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.maquinaPlanejada).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.modeloParametros).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.orcamentoCustoIndiretos).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.orcamentoProdutos).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.planejamentoCompras).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.planejamentoOperacao).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.safras).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.talhaos).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.configArea).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.financeiroContas).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.assinaturaContas).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.parceiros).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.principioAtivos).WithOne(c => c.conta).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
        }
    }
}