using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class OrganizacaoConfiguration : IEntityTypeConfiguration<Organizacao>
    {
        public void Configure(EntityTypeBuilder<Organizacao> builder)
        {
            builder.ToTable("Organizacoes");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Nome).HasMaxLength(128);
            builder.Property(x => x.Mascara).HasMaxLength(20);
            builder.Property(x => x.idconta);
            builder.Property(x => x.TipoPessoa);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.Property(x => x.Registro);
            builder.HasKey(x => new { x.Id });
            builder.HasIndex(x => x.idconta);
            builder.HasMany(x => x.fazendas).WithOne(x => x.organizacao).HasForeignKey(x => new { x.IdOrganizacao }).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.anosagricolas).WithOne(x => x.organizacao).HasForeignKey(x => new { x.IdOrganizacao }).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.organizacaoUsuarios).WithOne(x => x.organizacao).HasForeignKey(x => new { x.idorganizacao }).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.preferUsus).WithOne(x => x.organizacao).HasForeignKey(x => new { x.idorganizacao }).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.maquinas).WithOne(x => x.organizacao).HasForeignKey(x => x.idorganizacao).OnDelete(DeleteBehavior.NoAction);
        }
    }
}