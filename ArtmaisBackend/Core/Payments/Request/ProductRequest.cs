namespace ArtmaisBackend.Core.Payments.Request
{
    public class ProductRequest
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
