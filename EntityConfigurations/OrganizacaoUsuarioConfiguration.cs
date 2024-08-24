using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class OrganizacaoUsuarioConfiguration : IEntityTypeConfiguration<OrganizacaoUsuario>
    {
        public void Configure(EntityTypeBuilder<OrganizacaoUsuario> builder)
        {
            builder.ToTable("OrganizacaoUsuario");
            builder.Property(o => o.id).UseIdentityColumn();
            builder.Property(o => o.uid);
            builder.Property(o => o.idorganizacao);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uidlog).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.uid, x.idorganizacao });
        }
    }
}