using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class UsuarioContaConfiguration : IEntityTypeConfiguration<UsuarioConta>
    {
        public void Configure(EntityTypeBuilder<UsuarioConta> builder)
        {
            builder.ToTable("UsuarioConta");
            builder.Property(u => u.uid).IsRequired();
            builder.Property(u => u.contaguid).IsRequired();
            builder.Property(u => u.idconta).IsRequired();
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uidlog).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(u => (new { u.uid, u.contaguid, u.idconta }));
            builder.HasOne(u => u.conta).WithMany(u => u.usuarios).HasForeignKey(u => u.idconta);
        }
    }
}