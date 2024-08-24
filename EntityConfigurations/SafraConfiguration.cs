using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class SafraConfiguration : IEntityTypeConfiguration<Safra>
    {
        public void Configure(EntityTypeBuilder<Safra> builder)
        {
            builder.ToTable("Safras");
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CodigoExterno);
            builder.Property(x => x.Descricao).HasMaxLength(100);
            builder.Property(x => x.DataFim);
            builder.Property(x => x.DataInicio);
            builder.Property(x => x.IdCultura).IsRequired(false);
            builder.Property(x => x.Abertura);
            builder.Property(x => x.Reforma);
            builder.Property(x => x.IdAnoAgricola);
            builder.Property(x => x.idconta);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.Id, x.idconta });
            builder.HasOne(x => x.anoAgricola).WithMany(x => x.Safras)
                   .HasForeignKey(x => new { x.IdAnoAgricola, x.idconta }).OnDelete(DeleteBehavior.Cascade);
        }
    }
}