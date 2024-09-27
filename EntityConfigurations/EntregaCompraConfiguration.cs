using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class EntregaCompraConfiguration : IEntityTypeConfiguration<EntregaCompra>
    {
        public void Configure(EntityTypeBuilder<EntregaCompra> builder)
        {
            builder.ToTable("EntregaCompra");
            builder.Property(x => x.id);
            builder.Property(x => x.datains);
            builder.Property(x => x.dataup);
            builder.Property(x => x.idconta);
            builder.Property(x => x.uid);
            builder.Property(x => x.qtd);
            builder.Property(x => x.documento);
            builder.Property(x => x.dataentrega);
            builder.Property(x => x.idproduto);
            builder.Property(x => x.idprodutopedido);
            builder.HasKey(x => new { x.id, x.idprodutopedido, x.idconta });
            builder.HasOne(x => x.produtoCompra).WithMany(x => x.entregas).HasForeignKey(x => new { x.idprodutopedido, x.idconta, x.idpedido }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.produto).WithMany(x => x.entregas).HasForeignKey(x => new { x.idproduto, x.idconta }).OnDelete(DeleteBehavior.NoAction);
        }
    }
}