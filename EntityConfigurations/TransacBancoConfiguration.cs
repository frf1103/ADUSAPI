using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ADUSAPI.Entities;

namespace ADUSAPI.EntityConfigurations
{
    public class TransacBancoConfiguration : IEntityTypeConfiguration<TransacBanco>
    {
        public void Configure(EntityTypeBuilder<TransacBanco> builder)
        {
            builder.ToTable("TransacBanco");
            builder.Property(t => t.idtransacbanco).HasMaxLength(256);
            builder.Property(t => t.idbanco);
            builder.Property(t => t.idtransacao);
            builder.Property(t => t.idcategoria);
            builder.Property(t => t.idcentrocusto);
            builder.HasKey(t => new { t.idtransacbanco, t.idbanco });
            builder.HasOne(tb => tb.banco).WithMany(tb => tb.transacs).HasForeignKey(tb => tb.idbanco).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(tb => tb.transacao).WithMany(tb => tb.transacs).HasForeignKey(tb => tb.idtransacao).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(tb => tb.planoConta).WithMany(tb => tb.transacs).HasForeignKey(tb => tb.idcategoria).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(tb => tb.centroCusto).WithMany(tb => tb.transacs).HasForeignKey(tb => tb.idcentrocusto).OnDelete(DeleteBehavior.NoAction);
        }
    }
}