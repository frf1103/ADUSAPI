using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADUSAPI.EntityConfigurations
{
    public class BancoConfiguration : IEntityTypeConfiguration<Banco>
    {
        public void Configure(EntityTypeBuilder<Banco> builder)
        {
            builder.ToTable("Bancos");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                  .ValueGeneratedOnAdd(); // Idbuilder

            builder.Property(b => b.Descricao)
                  .IsRequired()
                  .HasMaxLength(50);

            builder.Property(b => b.Codigo)
                  .IsRequired()
                  .HasMaxLength(4);
        }
    }
}