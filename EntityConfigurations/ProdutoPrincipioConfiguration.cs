using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ProdutoPrincipioConfiguration : IEntityTypeConfiguration<ProdutoPrincipioAtivo>
    {
        public void Configure(EntityTypeBuilder<ProdutoPrincipioAtivo> builder)
        {
            builder.ToTable("ProdutoPrincipio");
            builder.Property(x => x.idproduto).IsRequired();
            builder.Property(x => x.idprincipio).IsRequired();
            builder.Property(x => x.quantidade).IsRequired();
            builder.Property(x => x.idconta).IsRequired();
            builder.HasKey(x => new { x.idconta, x.idproduto, x.idprincipio });

            builder.HasOne(x => x.conta).WithMany(x => x.produtoPrincipios).HasForeignKey(x => x.idconta);
        }
    }
}