using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADUSAPI.EntityConfigurations
{
    public class AssinaturaConfiguration : IEntityTypeConfiguration<Assinatura>
    {
        public void Configure(EntityTypeBuilder<Assinatura> builder)
        {
            builder.ToTable("Assinaturas");
            builder.Property(x => x.id).HasMaxLength(100);
            builder.Property(x => x.qtd);
            builder.Property(x => x.preco);
            builder.Property(x => x.valor);
            builder.Property(x => x.status);
            builder.Property(x => x.datavenda);
            builder.Property(x => x.idparceiro);
            builder.Property(x => x.idplataforma).HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.observacao).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.datains);
            builder.Property(x => x.dataup).IsRequired(false);
            builder.HasKey(x => x.id);
            builder.HasOne(x => x.parceiro).WithMany(x => x.assinaturas).HasForeignKey(x => x.idparceiro).OnDelete(DeleteBehavior.NoAction);
        }
    }
}