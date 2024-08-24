using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class EntregaContratoConfiguration : IEntityTypeConfiguration<EntregaContrato>
    {
        public void Configure(EntityTypeBuilder<EntregaContrato> builder)
        {
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(e => e.Quantidade);
            builder.Property(e => e.IdComercializacao);
            builder.Property(e => e.Documento);
            builder.Property(e => e.DataEntrega);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.Property(x => x.idconta);
            builder.HasKey(x => new { x.Id, x.idconta });
            builder.HasOne(e => e.comercializacao).WithMany(e => e.EntregaContrato).HasForeignKey(e => new { e.IdComercializacao, e.idconta });
        }
    }
}