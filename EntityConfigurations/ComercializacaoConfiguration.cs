using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ComercializacaoConfiguration : IEntityTypeConfiguration<Comercializacao>
    {
        public void Configure(EntityTypeBuilder<Comercializacao> builder)
        {
            builder.ToTable("Comercializacoes");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IdParceiro);
            builder.Property(x => x.IdSafra);
            builder.Property(x => x.IdMoeda);
            builder.Property(x => x.Quantidade);
            builder.Property(x => x.Trava);
            builder.Property(x => x.CBOT);
            builder.Property(x => x.Premio);
            builder.Property(x => x.Cambio);
            builder.Property(x => x.ValorUnitario);
            builder.Property(x => x.ValorTotal);
            builder.Property(x => x.DataEntrega);
            builder.Property(x => x.DataPagamento);
            builder.Property(x => x.Descontos);
            builder.Property(x => x.ValorLiquido);
            builder.Property(x => x.NumeroContrato);
            builder.Property(x => x.idconta);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.uid).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);

            builder.HasKey(x => new { x.Id, x.idconta });

            builder.HasOne(x => x.safra).WithMany(x => x.comercializacao).HasForeignKey(x => new { x.IdSafra, x.idconta });
            builder.HasOne(x => x.moeda).WithMany(x => x.comercializacao).HasForeignKey(x => new { x.IdMoeda });
            builder.HasOne(x => x.parceiro).WithMany(x => x.comercializacao).HasForeignKey(x => new { x.IdParceiro, x.idconta });
        }
    }
}