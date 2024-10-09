using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class PlanejamentoCompraConfiguration : IEntityTypeConfiguration<PlanejamentoCompra>
    {
        public void Configure(EntityTypeBuilder<PlanejamentoCompra> builder)
        {
            builder.ToTable("PlanejamentoCompras");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.QtdComprar);
            builder.Property(x => x.QtdComprada);
            builder.Property(x => x.idproduto);
            builder.Property(x => x.IdSafra);
            builder.Property(x => x.QtdEstoque);
            builder.Property(x => x.QtdNecessaria);
            builder.Property(x => x.Saldo);
            builder.Property(x => x.IdFazenda);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.Property(x => x.idconta);
            builder.HasKey(x => new { x.idconta, x.Id });
            builder.HasOne(x => x.produto).WithMany(x => x.planejamentoCompras)
                .HasForeignKey(x => new { x.idproduto, x.idconta })
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.safra).WithMany(x => x.planejamentoCompras)
                .HasForeignKey(x => new { x.IdSafra, x.idconta })
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.fazenda).WithMany(x => x.planejamentos)
                .HasForeignKey(x => new { x.IdFazenda, x.idconta })
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}