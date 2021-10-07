namespace ArtmaisBackend.Core.Aws.Request
{
    public class DeleteObjectCommand
    {
        public long UserId { get; private set; }
        public int PortfolioId { get; private set; }
        public string BucketName { get; set; }

        public DeleteObjectCommand(long userId, int portfolioId)
        {
            this.UserId = userId;
            this.PortfolioId = portfolioId;
            this.BucketName = "bucket-artmais";
        }
    }
}
