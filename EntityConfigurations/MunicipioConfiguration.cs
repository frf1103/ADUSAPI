using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADUSAPI.EntityConfigurations
{
    public class MunicipioConfiguration : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.ToTable("Municipios");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).HasMaxLength(60);
            builder.Property(x => x.CodigoIBGE).HasMaxLength(7);
            builder.Property(x => x.IdUF);
        }
    }
}