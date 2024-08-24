using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class OperacaoConfiguration : IEntityTypeConfiguration<Operacao>
    {
        public void Configure(EntityTypeBuilder<Operacao> builder)
        {
            builder.ToTable("Operacoes");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Insumo).IsRequired();
            builder.Property(x => x.Descricao).IsRequired();
            builder.Property(x => x.IdTipoOperacao).IsRequired();
            builder.Property(x => x.CodigoExterno).IsRequired(false);
            builder.Property(x => x.Rendimento).IsRequired(false);
            builder.Property(x => x.Consumo).IsRequired(false);
            builder.Property(x => x.idconta).IsRequired();
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.Id, x.idconta });
            builder.HasOne(x => x.TipoOperacao).WithMany(x => x.operacoes).HasForeignKey(x => new { x.IdTipoOperacao });
            builder.HasOne(x => x.conta).WithMany(x => x.operacoes).HasForeignKey(x => x.idconta);
        }
    }
}