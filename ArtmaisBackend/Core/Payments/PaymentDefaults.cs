namespace ArtmaisBackend.Core.Payments
{
    public static class PaymentDefaults
    {
        public const string NOTIFICATION_URL = "https://artmais-backend.herokuapp.com/v1/payment/updatePayment";
        public static readonly string PAYMENT_CREATED_SUBJECT = "Pagamento criado";
        public static readonly string PAYMENT_CREATED_MESSAGE = "Parabéns pela sua compra! Seu pagamento foi criado, te atualizaremos assim que houver alguma alteração em seu status.";
        public static readonly string PAYMENT_DONE_SUBJECT = "Pagamento aprovado";
        public static readonly string PAYMENT_DONE_MESSAGE = "Seu pagamento foi aprovado! Agora você já pode desfrutar dos seus benefícios de assinante na plataforma.";
        public static readonly string PAYMENT_PROCESSING_SUBJECT = "Pagamento em processamento";
        public static readonly string PAYMENT_PROCESSING_MESSAGE = "Seu pagamento está em processamento, te atualizaremos assim que houver alguma alteração em seu status.";
        public static readonly string PAYMENT_UNDONE_SUBJECT = "Pagamento negado";
        public static readonly string PAYMENT_UNDONE_MESSAGE = "Infelizmente o seu pedido foi negado, pedimos que entre em contato com o suporte da plataforma para mais informações.";
    }
}
