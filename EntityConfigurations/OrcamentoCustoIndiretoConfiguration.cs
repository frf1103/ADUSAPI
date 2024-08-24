using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class OrcamentoCustoIndiretoConfiguration : IEntityTypeConfiguration<OrcamentoCustoIndireto>
    {
        public void Configure(EntityTypeBuilder<OrcamentoCustoIndireto> builder)
        {
            builder.ToTable("OrcamentoCustoIndireto");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.idcontaCad);
            builder.Property(x => x.IdSafra);
            builder.Property(x => x.Data);
            builder.Property(x => x.valor);
            builder.Property(x => x.idconta);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.idconta, x.Id });
            builder.HasOne(x => x.contacad).WithMany(x => x.orcamentoCustoIndiretos).HasForeignKey(x => new { x.idcontaCad, x.idconta });
            builder.HasOne(x => x.safra).WithMany(x => x.orcamentoCustoIndiretos).HasForeignKey(x => x.IdSafra);
        }
    }
}