using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADUSAPI.EntityConfigurations
{
    public class LogCheckoutConfiguration : IEntityTypeConfiguration<LogCheckout>
    {
        public void Configure(EntityTypeBuilder<LogCheckout> builder)
        {
            builder.ToTable("LogCheckout");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.DataHora).IsRequired();
            builder.Property(e => e.NomeCliente).HasMaxLength(200);
            builder.Property(e => e.IpOrigem).HasMaxLength(50);
            builder.Property(e => e.TipoOperacao).HasMaxLength(50);
            builder.Property(e => e.UrlRequisicao).HasMaxLength(500);
            builder.Property(e => e.PayloadEnviado);
            builder.Property(e => e.RetornoApi);
            builder.Property(e => e.StatusHttp).HasMaxLength(20);
            builder.Property(e => e.idparcela).IsRequired(false);
            builder.Property(e => e.Erro).IsRequired(false);

            builder.HasOne(x => x.parcela).WithMany(c => c.logs).HasForeignKey(x => x.idparcela).OnDelete(DeleteBehavior.NoAction);
        }
    }
}