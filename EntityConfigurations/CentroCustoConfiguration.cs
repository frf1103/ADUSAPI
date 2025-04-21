using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADUSAPI.EntityConfigurations
{
    public class CentroCustoConfiguration : IEntityTypeConfiguration<CentroCusto>
    {
        public void Configure(EntityTypeBuilder<CentroCusto> builder)
        {
            builder.ToTable("CentroCusto");

            builder.HasKey(cc => cc.Id);
            builder.Property(cc => cc.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(cc => cc.Descricao)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(tc => tc.movimentoCaixas).WithOne(builder => builder.CentroCusto).HasForeignKey(m => m.IdCentroCusto).OnDelete(DeleteBehavior.NoAction);
        }
    }
}