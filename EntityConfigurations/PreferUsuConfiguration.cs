using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class PreferUsuConfiguration : IEntityTypeConfiguration<PreferUsu>
    {
        public void Configure(EntityTypeBuilder<PreferUsu> builder)
        {
            builder.ToTable("PreferUsu");
            builder.Property(p => p.Id).UseIdentityColumn(); ;
            builder.Property(p => p.uid).IsRequired();
            builder.Property(p => p.idorganizacao).IsRequired();
            builder.Property(p => p.idanoagricola).IsRequired();
            builder.Property(p => p.idconta).IsRequired();
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uidlog).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.idconta, x.Id });
        }
    }
}