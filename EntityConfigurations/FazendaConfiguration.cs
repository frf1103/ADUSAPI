using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class FazendaConfiguration : IEntityTypeConfiguration<Fazenda>
    {
        public void Configure(EntityTypeBuilder<Fazenda> builder)
        {
            builder.ToTable("Fazendas");
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CodigoExterno).IsRequired(false);
            builder.Property(x => x.Descricao).HasMaxLength(100);
            builder.Property(x => x.IdMunicipio);
            builder.Property(x => x.IdUF);
            builder.Property(x => x.IdOrganizacao);
            builder.Property(x => x.TipoArrenda);
            builder.Property(x => x.Tipo);
            builder.Property(x => x.IdRegiao);
            builder.Property(x => x.IdCultura).IsRequired(false);
            builder.Property(x => x.idconta);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.Id, x.idconta });
            builder.HasMany(x => x.Talhoes).WithOne(x => x.fazenda).HasForeignKey(x => new { x.IdFazenda, x.idconta })
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}