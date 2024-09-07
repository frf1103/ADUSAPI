using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ProdutoPlanejadoConfiguration : IEntityTypeConfiguration<ProdutoPlanejado>
    {
        public void Configure(EntityTypeBuilder<ProdutoPlanejado> builder)
        {
            builder.ToTable("ProdutosPlanejados");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IdPrincipioAtivo);
            builder.Property(x => x.IdProduto);

            builder.Property(x => x.IdPlanejamento);
            builder.Property(x => x.Dosagem);
            builder.Property(x => x.AreaPercent);
            builder.Property(x => x.Tamanho);
            builder.Property(x => x.TotalProduto);
            builder.Property(x => x.idconta);
            builder.HasKey(x => new { x.idconta, x.Id });
            builder.HasOne(x => x.produto).WithMany(x => x.produtosplanejados).HasForeignKey(x => new { x.IdProduto, x.idconta }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.principioativo).WithMany(x => x.produtosplanejados).HasForeignKey(x => new { x.IdPrincipioAtivo }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.planejamentoOperacao).WithMany(x => x.produtosplanejados).HasForeignKey(x => new { x.IdPlanejamento, x.idconta }).OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}