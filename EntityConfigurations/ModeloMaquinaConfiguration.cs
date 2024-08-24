using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ModeloMaquinaConfiguration : IEntityTypeConfiguration<ModeloMaquina>
    {
        public void Configure(EntityTypeBuilder<ModeloMaquina> builder)
        {
            builder.ToTable("ModelosMaquinas");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Combustivel);
            builder.Property(x => x.Descricao);
            builder.HasMany(x => x.maquinas).WithOne(x => x.modeloMaquina).HasForeignKey(x => x.IdModeloMaquina);
            builder.HasMany(x => x.modelosparametros).WithOne(x => x.modeloMaquina).HasForeignKey(x => x.IdModeloMaquina);
        }
    }
}
