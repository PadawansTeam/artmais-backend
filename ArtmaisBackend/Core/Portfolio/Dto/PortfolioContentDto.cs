using System;

namespace ArtmaisBackend.Core.Portfolio.Dto
{
    public class PortfolioContentDto
    {
        public long? UserID { get; set; }
        public int? PublicationID { get; set; }
        public int? MediaID { get; set; }
        public int? MediaTypeID { get; set; }
        public string? S3UrlMedia { get; set; }
        public string? Description { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}
