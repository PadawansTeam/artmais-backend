namespace ArtmaisBackend.Core.Publications.Request
{
    public class CommentRequest
    {
        public int? PublicationID { get; set; }
        public string Description { get; set; }
    }
}
