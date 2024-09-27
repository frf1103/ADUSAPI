using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ProdutoCompraConfiguration : IEntityTypeConfiguration<ProdutoCompra>
    {
        public void Configure(EntityTypeBuilder<ProdutoCompra> builder)
        {
            builder.ToTable("ProdutoCompra");
            builder.Property(p => p.id).IsRequired();
            builder.Property(p => p.idpedido).IsRequired();
            builder.Property(p => p.idproduto).IsRequired();
            builder.Property(p => p.qtdcompra).IsRequired();
            builder.Property(p => p.recebido).IsRequired();
            builder.Property(p => p.preco).IsRequired();
            builder.Property(p => p.total).IsRequired();
            builder.Property(p => p.idconta).IsRequired();
            builder.Property(p => p.uid);
            builder.Property(p => p.datains);
            builder.Property(p => p.dataup);
            builder.HasKey(p => new { p.id, p.idconta, p.idpedido });
            builder.HasOne(p => p.pedidocompra).WithMany(p => p.produtos).HasForeignKey(p => new { p.idpedido, p.idconta }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.produto).WithMany(p => p.produtospedidos).HasForeignKey(p => new { p.idproduto, p.idconta }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.conta).WithMany(p => p.produtospedidos).HasForeignKey(p => new { p.idconta }).OnDelete(DeleteBehavior.NoAction);
        }
    }
}