using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class MaquinaParametroConfiguration : IEntityTypeConfiguration<MaquinaParametro>
    {
        public void Configure(EntityTypeBuilder<MaquinaParametro> builder)
        {
            builder.ToTable("MaquinasParametros");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IdMaquina);
            builder.Property(x => x.IdCultura);
            builder.Property(x => x.Consumo);
            builder.Property(x => x.Rendimento);
            builder.Property(x => x.IdOperacao);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.Property(x => x.idconta);
            builder.HasKey(x => new { x.Id, x.idconta });

            builder.HasOne(x => x.operacao).WithMany(x => x.maquinasparametro).HasForeignKey(x => new { x.IdOperacao, x.idconta });
            builder.HasOne(x => x.cultura).WithMany(x => x.maquinasparametro).HasForeignKey(x => x.IdCultura);
            builder.HasOne(x => x.maquina).WithMany(x => x.maquinaParametros).HasForeignKey(x => new { x.IdMaquina, x.idconta });
        }
    }
}