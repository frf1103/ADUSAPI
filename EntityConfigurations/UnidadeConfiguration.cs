using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class UnidadeConfiguration : IEntityTypeConfiguration<Unidade>
    {
        public void Configure(EntityTypeBuilder<Unidade> builder)
        {
            builder.ToTable("Unidades");
            builder.HasKey(x => x.id);
            builder.Property(x => x.descricao).IsRequired();
            builder.Property(x => x.multiplo).IsRequired();

            builder.HasMany(x => x.produtos).WithOne(x => x.unidade).HasForeignKey(x => x.idunidade).OnDelete(DeleteBehavior.NoAction);
        }
    }
}