using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ConfigAreaConfiguration : IEntityTypeConfiguration<ConfigArea>
    {
        public void Configure(EntityTypeBuilder<ConfigArea> builder)
        {
            builder.ToTable("ConfigAreas");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IdSafra);
            builder.Property(x => x.IdTalhao);
            builder.Property(x => x.IdVariedade);
            builder.Property(x => x.Area);
            builder.Property(x => x.PMS).IsRequired(false);
            builder.Property(x => x.Stand).IsRequired(false);
            builder.Property(x => x.QtdSementePrevista).IsRequired(false);
            builder.Property(x => x.UnidadeSementePrevista).IsRequired(false);
            builder.Property(x => x.Espacamento).IsRequired(false);
            builder.Property(x => x.Germinacao).IsRequired(false);
            builder.Property(x => x.MargemSeguranca).IsRequired(false);
            builder.Property(x => x.PopulacaoRecomendada).IsRequired(false);
            builder.Property(x => x.ProdEstimada).IsRequired(false);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.Property(x => x.idconta);
            builder.HasKey(x => new { x.idconta, x.Id });

            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasOne(x => x.variedade).WithMany(x => x.configAreas)
                .HasForeignKey(x => x.IdVariedade).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.safra).WithMany(x => x.configAreas).
                HasForeignKey(x => new { x.IdSafra, x.idconta }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.talhao).WithMany(x => x.configAreas)
                .HasForeignKey(x => new { x.IdTalhao, x.idconta }).OnDelete(DeleteBehavior.NoAction);
        }
    }
}