using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class AssinaturaContaConfiguration : IEntityTypeConfiguration<AssinaturaConta>
    {
        public void Configure(EntityTypeBuilder<AssinaturaConta> builder)
        {
            builder.ToTable("AssinaturaConta");
            builder.HasKey(x => x.id);
            builder.Property(x => x.dataassinatura);
            builder.Property(x => x.dataexpiracao);
            builder.Property(x => x.idconta);
            builder.Property(x => x.plano);
        }
    }
}