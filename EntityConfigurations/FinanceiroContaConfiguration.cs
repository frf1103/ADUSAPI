using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class FinanceiroContaConfiguration : IEntityTypeConfiguration<FinanceiroConta>
    {
        public void Configure(EntityTypeBuilder<FinanceiroConta> builder)
        {
            builder.ToTable("FinanceiroConta");
            builder.HasKey(x => x.id);
            builder.Property(x => x.obs);
            builder.Property(x => x.emissao);
            builder.Property(x => x.vencimento);
            builder.Property(x => x.datapagto);
            builder.Property(x => x.tipo);
            builder.Property(x => x.idconta);
            builder.Property(x => x.desconto);
            builder.Property(x => x.valor);
            builder.Property(x => x.valorpago);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);
        }
    }
}