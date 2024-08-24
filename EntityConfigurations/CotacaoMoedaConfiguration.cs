using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class CotacaoMoedaConfiguration : IEntityTypeConfiguration<CotacaoMoeda>
    {
        public void Configure(EntityTypeBuilder<CotacaoMoeda> builder)
        {
            builder.ToTable("CotacoesMoeda");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdMoeda);
            builder.Property(x => x.CotacaoData);
            builder.Property(x => x.CotacaoValor);
            //builder.HasKey(x => new { x.IdMoeda, x.CotacaoData });
            builder.HasOne(x => x.moeda).WithMany(x => x.cotacaoMoedas).HasForeignKey(x => x.IdMoeda);
        }
    }
}