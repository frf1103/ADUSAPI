using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class VariedadeConfiguration : IEntityTypeConfiguration<Variedade>
    {
        public void Configure(EntityTypeBuilder<Variedade> builder)
        {
            builder.ToTable("Variedades");
            builder.HasKey(x => x.Id);
            

            builder.Property(x => x.CodigoExterno);
            builder.Property(x => x.Descricao).HasMaxLength(100);
            builder.Property(x => x.IdCultura);
            builder.Property(x => x.IdTecnologia);
            builder.Property(x => x.Ciclo);

        }
    }
}
