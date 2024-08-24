using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class GrupoContaConfiguration : IEntityTypeConfiguration<GrupoConta>
    {
        public void Configure(EntityTypeBuilder<GrupoConta> builder)
        {
            builder.ToTable("GruposContas");
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(t => t.IdClasseConta);
            builder.Property(t => t.Descricao);
            builder.Property(t => t.CodigoCliente).IsRequired(false);
            builder.Property(t => t.CodigoExterno).IsRequired(false);
            builder.Property(t => t.IdOrganizacao);
            builder.Property(x => x.idconta);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.Id, x.idconta });

            builder.HasOne(t => t.classeConta).WithMany(t => t.grupoContas).HasForeignKey(x => new { x.IdClasseConta }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.organizacao).WithMany(t => t.grupoContas).HasForeignKey(x => new { x.IdOrganizacao }).OnDelete(DeleteBehavior.NoAction);
        }
    }
}