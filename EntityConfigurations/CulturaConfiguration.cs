using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class CulturaConfiguration : IEntityTypeConfiguration<Cultura>
    {
        public void Configure(EntityTypeBuilder<Cultura> builder)
        {
            builder.ToTable("Culturas");
            builder.HasKey(x => x.Id);
            

            builder.Property(x => x.CodigoExterno);
            builder.Property(x => x.Descricao).HasMaxLength(100);
            builder.Property(x => x.NomeProduto).HasMaxLength(100);
            builder.Property(x => x.UnidadeProdutiva).HasMaxLength(10);
            builder.Property(x => x.DiasEstimadosEmergencia);
            builder.HasMany(x => x.Safras).WithOne(x => x.cultura).HasForeignKey(x => x.IdCultura);
            builder.HasMany(x => x.Variedades).WithOne(x => x.cultura).HasForeignKey(x => x.IdCultura);
            builder.HasMany(x => x.fazendas).WithOne(x => x.cultura).HasForeignKey(x => x.IdCultura);
        }
    }
}
