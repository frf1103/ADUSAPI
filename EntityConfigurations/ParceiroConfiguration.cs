using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ParceiroConfiguration : IEntityTypeConfiguration<Parceiro>
    {
        public void Configure(EntityTypeBuilder<Parceiro> builder)
        {
            builder.ToTable("Parceiros");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.RazaoSocial).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Fantasia).IsRequired().HasMaxLength(50);
            builder.Property(x => x.TipodePessoa).IsRequired();
            builder.Property(x => x.Registro).IsRequired();
            builder.Property(x => x.idconta).IsRequired();
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.Id, x.idconta });
        }
    }
}