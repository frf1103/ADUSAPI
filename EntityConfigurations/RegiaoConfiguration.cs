using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class RegiaoConfiguration : IEntityTypeConfiguration<Regiao>
    {
        public void Configure(EntityTypeBuilder<Regiao> builder)
        {
            builder.ToTable("Regioes");
            builder.HasKey(x => x.Id);
            

            
            builder.Property(x => x.Nome).HasMaxLength(50);
            builder.Property(x => x.Mascara).HasMaxLength(10);
            builder.HasMany(x => x.Fazendas).WithOne(x => x.regiao).HasForeignKey(x => x.IdRegiao);
            

        }
    }
}
