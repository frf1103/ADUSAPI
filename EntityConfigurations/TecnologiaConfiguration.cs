using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class TecnologiaConfiguration : IEntityTypeConfiguration<Tecnologia>
    {
        public void Configure(EntityTypeBuilder<Tecnologia> builder)
        {
            builder.ToTable("Tecnologias");
            builder.HasKey(x => x.Id);
            

            
            builder.Property(x => x.Descricao).HasMaxLength(100);
            builder.HasMany(x=>x.Variedades).WithOne(x=>x.tecnologia).HasForeignKey(x=>x.IdTecnologia);
        }
    }
}
