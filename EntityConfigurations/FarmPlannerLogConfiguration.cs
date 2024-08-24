using FarmPlannerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPlannerAPI.EntityConfigurations
{
    public class FarmPlannerLogConfiguration : IEntityTypeConfiguration<FarmPlannerLog>
    {
        public void Configure(EntityTypeBuilder<FarmPlannerLog> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.transacao);
            builder.Property(x => x.uid);
            builder.Property(x => x.transacao);
            builder.Property(x => x.datalog);
        }
    }
}