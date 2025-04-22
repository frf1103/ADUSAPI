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
            builder.Property(x => x.token).IsRequired().HasMaxLength(500);
            builder.Property(x => x.ultdata).IsRequired();
            builder.Property(x => x.urlsub).IsRequired().HasMaxLength(500);
            builder.Property(x => x.urltransac).IsRequired().HasMaxLength(500);
            builder.Property(x => x.idcategoria);
            builder.Property(x => x.idtransacao);
            builder.Property(x => x.idconta);
            builder.Property(x => x.idccusto);
            builder.Property(x => x.idparceiro);
            builder.HasOne(x => x.Transacao).WithMany(x => x.parametros).HasForeignKey(x => x.idtransacao).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.parceiro).WithMany(x => x.parametros).HasForeignKey(x => x.idparceiro).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ContaCorrente).WithMany(x => x.parametros).HasForeignKey(x => x.idconta).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Categoria).WithMany(x => x.parametros).HasForeignKey(x => x.idcategoria).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.CentroCusto).WithMany(x => x.parametros).HasForeignKey(x => x.idccusto).OnDelete(DeleteBehavior.NoAction);

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
            builder.HasData(
               new ParametrosGuru
               {
                   id = 2,
                   token = "$aact_prod_000MzkwODA2MWY2OGM3MWRlMDU2NWM3MzJlNzZmNGZhZGY6OmIyNmM2YWIzLThmOGUtNDY5Mi1hNDNkLWJiNDk4YTRmNGNjOTo6JGFhY2hfZTI5MTFhMGMtYjdkNi00MzhlLWI2OTEtOTYxNzYzMmI2NDBk",
                   ultdata = Convert.ToDateTime("2024-10-01"),
                   urlsub = " ",
                   urltransac = "https://api.asaas.com/v3/payments"
               }
           );

            builder.HasData(
               new ParametrosGuru
               {
                   id = 3,
                   token = "sk_871c3d7c606a4be3bd48d4f86b68c58f",
                   ultdata = Convert.ToDateTime("2024-10-01"),
                   urlsub = " ",
                   urltransac = "https://api.pagar.me/core/v5/payables"
               }
           );
        }
    }
}