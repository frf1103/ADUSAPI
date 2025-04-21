using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADUSAPI.Configurations
{
    public class MovimentoCaixaConfiguration : IEntityTypeConfiguration<MovimentoCaixa>
    {
        public void Configure(EntityTypeBuilder<MovimentoCaixa> builder)
        {
            builder.ToTable("MovimentoCaixa");

            builder.HasKey(mc => mc.Id);

            builder.Property(mc => mc.Sinal)
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(mc => mc.Observacao)
                .HasMaxLength(1000);

            builder.Property(mc => mc.Valor)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder.Property(mc => mc.DataMov)
                .HasColumnType("datetime")
                .IsRequired();

            // Relacionamentos
        }
    }
}