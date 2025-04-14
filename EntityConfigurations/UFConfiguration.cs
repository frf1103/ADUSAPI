using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADUSAPI.EntityConfigurations
{
    public class UFConfiguration : IEntityTypeConfiguration<UF>
    {
        public void Configure(EntityTypeBuilder<UF> builder)
        {
            builder.ToTable("UFs");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).HasMaxLength(60);
            builder.Property(x => x.CodigoIBGE).HasMaxLength(2);
            builder.Property(x => x.Sigla).HasMaxLength(2);

            builder.HasMany(x => x.Municipios).WithOne(x => x.uF).HasForeignKey(x => x.IdUF);
        }
    }
}