using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class PlanejamentoOperacaoConfiguration : IEntityTypeConfiguration<PlanejamentoOperacao>
    {
        public void Configure(EntityTypeBuilder<PlanejamentoOperacao> builder)
        {
            builder.ToTable("PlanejamentoOperacoes");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Plantio);
            builder.Property(x => x.DAE);
            builder.Property(x => x.Status);
            builder.Property(x => x.DataPrevista);
            builder.Property(x => x.Area);
            builder.Property(x => x.CustoOperacao);
            builder.Property(x => x.IdConfigArea);
            builder.Property(x => x.IdOperacao);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.Property(x => x.idconta);
            builder.HasKey(x => new { x.Id, x.idconta });
            //builder.Property(x => x.IdOrcamento);
            builder.Property(x => x.QCombustivelEstimado);
            builder.Property(x => x.QHorasEstimadas);
            builder.HasOne(x => x.configArea).WithMany(x => x.planejamentoOperacao).HasForeignKey(x => new { x.idconta, x.IdConfigArea });
            //  builder.HasOne(x => x.orcamentoProduto).WithMany(x => x.planejamentoOperacao).HasForeignKey(x => x.IdOrcamento);
            builder.HasOne(x => x.operacao).WithMany(x => x.planejamentoOperacao).HasForeignKey(x => new { x.IdOperacao, x.idconta });
        }
    }
}