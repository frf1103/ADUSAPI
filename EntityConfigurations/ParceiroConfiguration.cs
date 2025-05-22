using ADUSAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADUSAPI.EntityConfigurations
{
    public class ParceiroConfiguration : IEntityTypeConfiguration<Parceiro>
    {
        public void Configure(EntityTypeBuilder<Parceiro> builder)
        {
            builder.ToTable("Parceiros");
            builder.HasKey(x => x.uid);
            builder.Property(x => x.RazaoSocial).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Fantasia).IsRequired().HasMaxLength(50);
            builder.Property(x => x.TipodePessoa).IsRequired();
            builder.Property(x => x.Registro).IsRequired();

            builder.Property(x => x.CEP).IsRequired().HasMaxLength(8);
            builder.Property(x => x.Logradouro).IsRequired().HasMaxLength(120);
            builder.Property(x => x.Numero).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Bairro).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Complemento).IsRequired(false);

            builder.Property(x => x.IdRepresentante).IsRequired(false);
            builder.Property(x => x.Profissao).IsRequired(false);
            builder.Property(x => x.idCidade).IsRequired();
            builder.Property(x => x.idUF).IsRequired();
            builder.Property(x => x.DtNascimento).IsRequired();
            builder.Property(x => x.Fone1).IsRequired(false);
            builder.Property(x => x.Fone2).IsRequired(false);
            builder.Property(x => x.email).IsRequired(false);
            builder.Property(x => x.observacao).IsRequired(false);
            builder.Property(x => x.EstadoCivil).IsRequired(true);
            builder.Property(x => x.datains).IsRequired(false);
            builder.Property(x => x.dataup).IsRequired(false);
            builder.Property(x => x.Sexo).IsRequired(true).HasMaxLength(1);
            builder.Property(x => x.isafiliado).IsRequired(false).HasDefaultValue(false);
            builder.Property(x => x.iscoprodutor).IsRequired(false).HasDefaultValue(false);
            builder.Property(x => x.isbanco).IsRequired(false).HasDefaultValue(false);
            builder.Property(x => x.isassinante).IsRequired(false).HasDefaultValue(true);
            builder.Property(x => x.idcoprodutor).IsRequired(false);
            builder.Property(x => x.percomissao).IsRequired(false);
            builder.Property(x => x.iduser).IsRequired(false);
            builder.Property(x => x.urlafiliado).HasMaxLength(300);
            builder.HasOne(x => x.Representante).WithMany(x => x.empresas).HasForeignKey(x => x.IdRepresentante).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.cidade).WithMany(x => x.parceiros).HasForeignKey(x => x.idCidade).OnDelete(DeleteBehavior.NoAction); ;
            builder.HasOne(x => x.uf).WithMany(x => x.Parceiros).HasForeignKey(x => x.idUF).OnDelete(DeleteBehavior.NoAction); ;
            builder.HasMany(tc => tc.movimentoCaixas).WithOne(builder => builder.parceiro).HasForeignKey(m => m.idparceiro).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.coprodutor).WithMany(x => x.afiliados).HasForeignKey(x => x.idcoprodutor).OnDelete(DeleteBehavior.NoAction);
        }
    }
}