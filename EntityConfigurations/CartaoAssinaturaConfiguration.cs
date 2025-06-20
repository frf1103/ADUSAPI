using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.EntityConfigurations
{
    public class CartaoAssinaturaConfiguration : IEntityTypeConfiguration<CartaoAssinatura>
    {
        public void Configure(EntityTypeBuilder<CartaoAssinatura> builder)
        {
            // Chave primária
            builder.HasKey(c => c.Id);

            // Propriedades de texto com tamanho máximo e obrigatoriedade
            builder.Property(c => c.IdToken)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(c => c.UltimosDigitos)
                   .HasMaxLength(4)
                   .IsRequired();
            builder.Property(c => c.Bandeira)
                   .HasMaxLength(50)
                   .IsRequired();

            // Propriedade de chave estrangeira (FK) - obrigatória
            builder.Property(c => c.IdAssinatura)
                   .IsRequired();

            // Configuração do relacionamento muitos-para-um com Assinatura
            builder.HasOne(c => c.Assinatura)
                   .WithMany(a => a.cartoes)
                   .HasForeignKey(c => c.IdAssinatura);
        }
    }
}