using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class PedidoCompraConfiguration : IEntityTypeConfiguration<PedidoCompra>
    {
        public void Configure(EntityTypeBuilder<PedidoCompra> builder)
        {
            builder.ToTable("PedidoCompra");
            builder.Property(p => p.id);
            builder.Property(p => p.idconta);
            builder.Property(p => p.vencimento);
            builder.Property(p => p.idsafra);
            builder.Property(p => p.idfazenda);
            builder.Property(p => p.idfornecedor);
            builder.Property(p => p.pedidofornecedor).IsRequired(false).HasMaxLength(20);
            builder.Property(p => p.total);
            builder.Property(p => p.datapedido);
            builder.Property(p => p.datains);
            builder.Property(p => p.dataup).IsRequired(false);
            builder.Property(p => p.uid);
            builder.Property(p => p.idmoeda);
            builder.HasKey(p => new { p.id, p.idconta });
            builder.HasOne(p => p.safra).WithMany(p => p.pedidos).HasForeignKey(p => new { p.idsafra, p.idconta }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.fazenda).WithMany(p => p.pedidos).HasForeignKey(p => new { p.idfazenda, p.idconta }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.parceiro).WithMany(p => p.pedidos).HasForeignKey(p => new { p.idfornecedor, p.idconta }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.moeda).WithMany(p => p.pedidos).HasForeignKey(p => new { p.idmoeda }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.conta).WithMany(p => p.pedidos).HasForeignKey(p => new { p.idconta }).OnDelete(DeleteBehavior.NoAction);
        }
    }
}