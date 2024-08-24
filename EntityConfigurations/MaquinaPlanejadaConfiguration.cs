using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class MaquinaPlanejadaConfiguration : IEntityTypeConfiguration<MaquinaPlanejada>
    {
        public void Configure(EntityTypeBuilder<MaquinaPlanejada> builder)
        {
            builder.ToTable("MaquinasPlanejadas");
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(t => t.IdMaquina);
            builder.Property(t => t.IdModeloMaquina);
            builder.Property(t => t.QtdCombEstimado);
            builder.Property(t => t.Consumo);
            builder.Property(t => t.IdPlanejamento);
            builder.Property(t => t.QtdHoraEstimada);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.Property(t => t.Rendimento);
            builder.Property(x => x.idconta);
            builder.HasKey(x => new { x.idconta, x.Id });
            builder.HasOne(t => t.modelomaquina).WithMany(t => t.maquinasplanejada).HasForeignKey(m => new { m.IdModeloMaquina }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.maquina).WithMany(t => t.maquinasplanejada).HasForeignKey(m => new { m.IdMaquina, m.idconta }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.planejamentoOperacao).WithMany(t => t.maquinasplanejada).HasForeignKey(m => new { m.IdPlanejamento, m.idconta });
        }
    }
}