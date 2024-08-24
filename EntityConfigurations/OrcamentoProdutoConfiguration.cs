using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class OrcamentoProdutoConfiguration : IEntityTypeConfiguration<OrcamentoProduto>
    {
        public void Configure(EntityTypeBuilder<OrcamentoProduto> builder)
        {
            builder.ToTable("OrcamentoProdutos");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Descricao).IsRequired();
            builder.Property(x => x.IdSafra).IsRequired();
            builder.Property(x => x.IdFazenda).IsRequired();
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.Property(x => x.idconta);
            builder.HasKey(x => new { x.Id, x.idconta });
            builder.HasOne(x => x.fazenda).WithMany(x => x.OrcamentoProduto).HasForeignKey(x => new { x.IdFazenda, x.idconta }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.safra).WithMany(x => x.OrcamentoProduto).HasForeignKey(x => new { x.IdSafra, x.idconta }).OnDelete(DeleteBehavior.NoAction);
        }
    }
}