using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ProdutoOrcamentoConfiguration : IEntityTypeConfiguration<ProdutoOrcamento>
    {
        public void Configure(EntityTypeBuilder<ProdutoOrcamento> builder)
        {
            builder.ToTable("ProdutosOrcamento");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IdPrincipioAtivo);
            builder.Property(x => x.IdProduto).IsRequired(false);
            builder.Property(x => x.TipoProdutoOrc);
            builder.Property(x => x.PrecoUnitario);
            builder.Property(x => x.DataPreco).IsRequired(false);
            builder.Property(x => x.idconta);
            builder.Property(x => x.idmoeda);
            builder.HasKey(x => new { x.idconta, x.Id });
            builder.HasOne(x => x.orcamentoProduto).WithMany(x => x.produtoorcamento).HasForeignKey(x => new { x.IdOrcamento, x.idconta });
            builder.HasOne(x => x.produto).WithMany(x => x.produtoorcamento).HasForeignKey(x => new { x.IdProduto, x.idconta }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.princativo).WithMany(x => x.produtoorcamento).HasForeignKey(x => new { x.IdPrincipioAtivo }).OnDelete(DeleteBehavior.NoAction); ;
            builder.HasOne(x => x.moeda).WithMany(x => x.produtosorcamento).HasForeignKey(x => new { x.idmoeda }).OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}