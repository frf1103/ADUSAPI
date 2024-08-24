using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class CadastroContaConfiguration : IEntityTypeConfiguration<CadastroConta>
    {
        public void Configure(EntityTypeBuilder<CadastroConta> builder)
        {
            builder.ToTable("CadastroContas");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IdGrupoConta);
            builder.Property(x => x.Descricao);
            builder.Property(x => x.CodigoCliente);
            builder.Property(x => x.CodigoExterno);
            builder.Property(x => x.idconta);
            builder.HasKey(x => new { x.Id, x.idconta });
            builder.HasOne(x => x.grupoConta).WithMany(x => x.cadastroContas).HasForeignKey(x => new { x.IdGrupoConta, x.idconta });
        }
    }
}