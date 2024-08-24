using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class MarcaMaquinaConfiguration : IEntityTypeConfiguration<MarcaMaquina>
    {
        public void Configure(EntityTypeBuilder<MarcaMaquina> builder)
        {
            builder.ToTable("MarcasMaquinas");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descricao);
            builder.HasMany(x=>x.ModeloMaquina).WithOne(x=>x.marcaMaquina).HasForeignKey(x=>x.IdMarca);
        }
    }
}
