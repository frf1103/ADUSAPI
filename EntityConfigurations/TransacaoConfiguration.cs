using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.EntityConfigurations
{
    public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Sinal)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(t => t.Contrapartida)
                .IsRequired()
                .HasConversion<int>(); // Enum como inteiro
            builder.HasMany(tc => tc.movs).WithOne(builder => builder.Transacao).HasForeignKey(m => m.IdTransacao).OnDelete(DeleteBehavior.NoAction);
        }
    }
}