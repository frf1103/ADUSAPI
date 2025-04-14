using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADUSAPI.EntityConfigurations
{
    public class ParametrosGuruConfiguration : IEntityTypeConfiguration<ParametrosGuru>
    {
        public void Configure(EntityTypeBuilder<ParametrosGuru> builder)
        {
            builder.ToTable("ParametrosGuru");
            builder.HasKey(x => x.id);
            builder.Property(x => x.token).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ultdata).IsRequired();
            builder.Property(x => x.urlsub).IsRequired().HasMaxLength(256);
            builder.Property(x => x.urltransac).IsRequired().HasMaxLength(256);
            builder.HasData(
                new ParametrosGuru
                {
                    id = 1,
                    token = "9e883bc2-e356-440e-b28b-327532ace5d2|LuSXsELCchQMjGN0A9CICbQJCLwhglsstYRPPVr57fb50393",
                    ultdata = Convert.ToDateTime("2024-10-01"),
                    urlsub = "https://digitalmanager.guru/api/v2/subscriptions",
                    urltransac = "https://digitalmanager.guru/api/v2/transactions"
                }
            );
        }
    }
}