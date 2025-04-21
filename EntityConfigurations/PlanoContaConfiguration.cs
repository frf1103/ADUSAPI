using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.EntityConfigurations
{
    public class PlanoContaConfiguration : IEntityTypeConfiguration<PlanoConta>
    {
        public void Configure(EntityTypeBuilder<PlanoConta> builder)
        {
            builder.ToTable("PlanoConta");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Descricao)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Sinal)
                   .IsRequired()
                   .HasMaxLength(1);

            builder.Property(p => p.IdMae)
                   .IsRequired(false);

            // Autorelacionamento
            builder.HasOne(p => p.Mae)
                   .WithMany(p => p.Filhos)
                   .HasForeignKey(p => p.IdMae)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(tc => tc.movimentoCaixas).WithOne(builder => builder.Categoria).HasForeignKey(m => m.IdCategoria).OnDelete(DeleteBehavior.NoAction);
        }
    }
}