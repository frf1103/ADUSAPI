using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class AnoAgricolaConfiguration : IEntityTypeConfiguration<AnoAgricola>
    {
        public void Configure(EntityTypeBuilder<AnoAgricola> builder)
        {
            builder.ToTable("AnosAgricolas");
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.IdOrganizacao);
            builder.Property(x => x.CodigoExterno);
            builder.Property(x => x.Descricao).HasMaxLength(100);
            builder.Property(x => x.DataInicio);
            builder.Property(x => x.DataFim);
            builder.Property(x => x.idconta);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);
            builder.HasKey(x => new { x.Id, x.idconta });
            builder.HasMany(x => x.preferUsus).WithOne(x => x.anoAgricola).HasForeignKey(x => new { x.idanoagricola, x.idconta }).OnDelete(DeleteBehavior.NoAction);
            /*           builder.HasMany(x => x.Talhoes).WithOne(x => x.ano)
                           .HasForeignKey(x => x.IdAnoAgricola)
                           .OnDelete(DeleteBehavior.NoAction);
                       builder.HasMany(x => x.Safras).WithOne(x => x.anoAgricola)
                           .HasForeignKey(x => x.IdAnoAgricola)
                           .OnDelete(DeleteBehavior.NoAction);
             */
        }
    }
}