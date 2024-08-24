using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class TipoOperacaoConfiguration : IEntityTypeConfiguration<TipoOperacao>
    {
     
        public void Configure(EntityTypeBuilder<TipoOperacao> builder)
        {
            builder.ToTable("TiposOperacoes");
            builder.HasKey(t => t.Id);
            builder.Property(t=>t.Descricao).IsRequired();
        }
    }
}
