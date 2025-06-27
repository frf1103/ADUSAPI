namespace ADUSAPI.Shared
{
    public class WebhookPayload
    {
        public string @event { get; set; }
        public Payment payment { get; set; }
        public DateTime dateCreated { get; set; }
    }

    public class Payment
    {
        public string objectType { get; set; }
        public string id { get; set; }
        public DateTime dateCreated { get; set; }
        public string customer { get; set; }
        public string subscription { get; set; }
        public string billingType { get; set; }
        public decimal value { get; set; }
        public decimal? netValue { get; set; }
        public string description { get; set; }
        public bool canBePaidAfterDueDate { get; set; }
        public string status { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime originalDueDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public DateTime? clientPaymentDate { get; set; }
        public int? installmentNumber { get; set; }
        public string invoiceUrl { get; set; }
        public string bankSlipUrl { get; set; }
        public string nossoNumero { get; set; }
        public Discount discount { get; set; }
        public Fine fine { get; set; }
        public Interest interest { get; set; }
        public bool postalService { get; set; }
        public bool deleted { get; set; }
        public PixTransaction pixTransaction { get; set; }
    }

    public class Discount
    {
        public decimal value { get; set; }
        public DateTime? limitDate { get; set; }
        public int dueDateLimitDays { get; set; }
        public string type { get; set; }
    }

    public class Fine
    {
        public decimal value { get; set; }
        public string type { get; set; }
    }

    public class Interest
    {
        public decimal value { get; set; }
        public string type { get; set; }
    }

    public class PixTransaction
    {
        public string endToEndId { get; set; }
        public string qrCode { get; set; }
        public string payload { get; set; }
    }
}