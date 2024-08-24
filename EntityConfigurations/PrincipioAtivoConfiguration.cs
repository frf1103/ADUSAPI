using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class PrincipioAtivoConfiguration : IEntityTypeConfiguration<PrincipioAtivo>
    {
        public void Configure(EntityTypeBuilder<PrincipioAtivo> builder)
        {
            builder.ToTable("PrincipiosAtivos");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(100);
            builder.Property(x => x.idconta).IsRequired();
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.idconta, x.Id });
        }
    }
}