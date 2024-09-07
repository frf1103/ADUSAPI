using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(100);
            builder.Property(x => x.IdGrupoProduto).IsRequired();
            //   builder.Property(x => x.IdPrincipioAtivo).IsRequired();
            builder.Property(x => x.IdFabricante).IsRequired();
            builder.Property(x => x.idunidade).IsRequired();
            builder.Property(x => x.idconta);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.Id, x.idconta });

            //     builder.HasOne(x => x.principioAtivo).WithMany(x => x.Produtos).HasForeignKey(x => new { x.idconta, x.IdPrincipioAtivo });
            builder.HasOne(x => x.grupoProduto).WithMany(x => x.Produtos).HasForeignKey(x => new { x.IdGrupoProduto });
            builder.HasOne(x => x.parceiro).WithMany(x => x.Produtos).HasForeignKey(x => new { x.IdFabricante, x.idconta });
            builder.HasOne(x => x.conta).WithMany(x => x.produtos).HasForeignKey(x => x.idconta);
            builder.HasMany(x => x.produtosprincipio).WithOne(x => x.produtoproduto).HasForeignKey(x => new { x.idproduto, x.idconta }).OnDelete(DeleteBehavior.NoAction);
        }
    }
}