using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.EntityConfigurations
{
    public class ConviteConfiguration : IEntityTypeConfiguration<Convite>
    {
        public void Configure(EntityTypeBuilder<Convite> builder)
        {
            builder.ToTable("Convites");

            builder.HasKey(c => c.IdConvite);

            builder.Property(c => c.Fone).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(255);
            builder.Property(c => c.DataCriacao).IsRequired();
            builder.Property(c => c.DataExpiracao).IsRequired();
            builder.Property(c => c.IdAfiliado).IsRequired();
            builder.Property(c => c.IdPlataforma).IsRequired();
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.idassinatura);
            builder.Property(c => c.Titular);
            builder.Property(x => x.idformapgto).HasMaxLength(20);
            builder.HasOne(c => c.afiliado).WithMany(x => x.convites).HasForeignKey(x => x.IdAfiliado).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.assinatura).WithOne(x => x.convite).HasForeignKey<Convite>(c => c.idassinatura).OnDelete(DeleteBehavior.NoAction);
        }
    }
}