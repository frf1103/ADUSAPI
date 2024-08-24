using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ModeloParametroConfiguration : IEntityTypeConfiguration<ModeloParametro>
    {
        public void Configure(EntityTypeBuilder<ModeloParametro> builder)
        {
            builder.ToTable("ModelosParametros");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IdCultura);
            builder.Property(x => x.IdOperacao);
            builder.Property(x => x.Consumo);
            builder.Property(x => x.Rendimento);
            builder.Property(x => x.IdModeloMaquina);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.Property(x => x.idconta);
            builder.HasKey(x => new { x.idconta, x.Id });
            builder.HasOne(x => x.cultura).WithMany(x => x.modelosparametro).HasForeignKey(x => x.IdCultura);
            builder.HasOne(x => x.operacao).WithMany(x => x.modelosparametro).HasForeignKey(x => new { x.IdOperacao, x.idconta }).OnDelete(DeleteBehavior.NoAction);
        }
    }
}