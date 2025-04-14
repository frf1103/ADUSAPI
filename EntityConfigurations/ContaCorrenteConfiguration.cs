using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ADUSAPI.EntityConfigurations
{
    public class ContaCorrenteConfiguration : IEntityTypeConfiguration<ContaCorrente>
    {
        public void Configure(EntityTypeBuilder<ContaCorrente> builder)
        {
            builder.ToTable("ContaCorrente");

            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Id)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(cc => cc.Descricao)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(cc => cc.Agencia)
                .HasMaxLength(4)
                .IsRequired();

            builder.Property(cc => cc.ContaBanco)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(cc => cc.Titular)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(cc => cc.Banco)
                .WithMany(b => b.ContasCorrentes)
                .HasForeignKey(cc => cc.BancoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}