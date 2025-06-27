using ADUSAPI.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADUSAPI.EntityConfigurations
{
    public class WebhookAsaasConfiguration : IEntityTypeConfiguration<WebhookAsaas>
    {
        public void Configure(EntityTypeBuilder<WebhookAsaas> builder)
        {
            builder.ToTable("WebhookAsaas");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Evento).HasMaxLength(100);
            builder.Property(x => x.PaymentId).HasMaxLength(50);
            builder.Property(x => x.SubscriptionId).HasMaxLength(50);
            builder.Property(x => x.CustomerId).HasMaxLength(50);
            builder.Property(x => x.Status).HasMaxLength(50);
            builder.Property(x => x.BillingType).HasMaxLength(50);
            builder.Property(x => x.JsonCompleto).HasColumnType("nvarchar(max)");
        }
    }
}