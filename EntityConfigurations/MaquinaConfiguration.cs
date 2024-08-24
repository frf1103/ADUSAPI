using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class MaquinaConfiguration : IEntityTypeConfiguration<Maquina>
    {
        public void Configure(EntityTypeBuilder<Maquina> builder)
        {
            builder.ToTable("Maquinas");
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Descricao);
            builder.Property(e => e.Ano);
            builder.Property(e => e.CodigoExterno);
            builder.Property(e => e.idconta);
            builder.Property(e => e.idorganizacao);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.Id, x.idconta });
        }
    }
}