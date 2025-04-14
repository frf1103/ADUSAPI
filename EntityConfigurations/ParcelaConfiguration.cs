using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADUSAPI.EntityConfigurations
{
    public class ParcelaConfiguration : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.ToTable("Parcelas");
            builder.Property(x => x.id).HasMaxLength(100);
            builder.HasKey(x => x.id);
            builder.Property(x => x.idassinatura).HasMaxLength(100);
            builder.Property(x => x.numparcela);
            builder.Property(x => x.idcaixa).IsRequired(false);
            builder.Property(x => x.idformapagto);
            builder.Property(x => x.datavencimento);
            builder.Property(x => x.databaixa);
            builder.Property(x => x.plataforma);
            builder.Property(x => x.valor);
            builder.Property(x => x.comissao);
            builder.Property(x => x.descontoplataforma);
            builder.Property(x => x.valorliquido);
            builder.Property(x => x.idcheckout).IsRequired(false).HasMaxLength(256);
            builder.Property(x => x.nossonumero).IsRequired(false).HasMaxLength(256);
            builder.Property(x => x.datains);
            builder.Property(x => x.dataup);
            builder.Property(x => x.acrescimos);
            builder.Property(x => x.descontos);
            builder.Property(x => x.observacao).HasMaxLength(1000).IsRequired(false);
            builder.HasOne(x => x.assinatura).WithMany(x => x.parcelas).HasForeignKey(x => x.idassinatura).OnDelete(DeleteBehavior.NoAction);
        }
    }
}