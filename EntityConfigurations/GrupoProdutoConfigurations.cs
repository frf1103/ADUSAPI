using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class GrupoProdutoConfigurations : IEntityTypeConfiguration<GrupoProduto>
    {
        public void Configure(EntityTypeBuilder<GrupoProduto> builder)
        {
            builder.ToTable("GruposProdutos");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Tipo);

            //builder.HasMany(x => x.Produtos).WithOne(x=>x.grupoProduto).HasForeignKey(x=>x.IdGrupoProduto);
        }
    }
}