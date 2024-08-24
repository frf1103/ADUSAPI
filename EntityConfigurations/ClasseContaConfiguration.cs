using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class ClasseContaConfiguration : IEntityTypeConfiguration<ClasseConta>
    {
        public void Configure(EntityTypeBuilder<ClasseConta> builder)
        {
            builder.ToTable("ClassesContas");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descricao);
            builder.Property(x => x.TipoClasseConta).IsRequired();
        }
    }
}