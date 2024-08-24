using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class TalhaoConfiguration : IEntityTypeConfiguration<Talhao>
    {
        public void Configure(EntityTypeBuilder<Talhao> builder)
        {
            builder.ToTable("Talhoes");
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CodigoExterno).IsRequired(false);
            builder.Property(x => x.Descricao).HasMaxLength(100);
            builder.Property(x => x.TipoArea);
            builder.Property(x => x.AreaProdutiva);
            builder.Property(x => x.IdFazenda);
            builder.Property(x => x.IdAnoAgricola);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.Property(x => x.idconta);
            builder.HasKey(x => new { x.Id, x.idconta });
            builder.HasOne(x => x.anoagricola).WithMany(x => x.Talhoes)
                   .HasForeignKey(x => new { x.IdAnoAgricola, x.idconta })
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}