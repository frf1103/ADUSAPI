namespace ADUSAPI.Entities
{
    public class WebhookAsaas
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Evento { get; set; }
        public string PaymentId { get; set; }
        public string SubscriptionId { get; set; }
        public string CustomerId { get; set; }
        public string Status { get; set; }
        public string BillingType { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public string JsonCompleto { get; set; }
        public DateTime DataRecebimento { get; set; } = DateTime.Now;
    }
}