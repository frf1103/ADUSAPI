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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(100);

            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasMany(x => x.produtosprincipio).WithOne(x => x.principioAtivo).HasForeignKey(x => new { x.idprincipio }).OnDelete(DeleteBehavior.NoAction);
        }
    }
}