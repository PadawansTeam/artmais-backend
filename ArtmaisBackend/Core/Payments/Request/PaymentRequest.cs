namespace ArtmaisBackend.Core.Payments.Request
{
    public class PaymentRequest
    {
        public decimal? TransactionAmount { get; set; }
        public string CardToken { get; set; }
        public string Description { get; set; }
        public int Installments { get; set; }
        public string PaymentMethodId { get; set; }
        public string Email { get; set; }
    }
}
