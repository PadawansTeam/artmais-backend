using System;

namespace ArtmaisBackend.Core.Portfolio.Dto
{
    public class PortfolioContentDto
    {
        public long? UserId { get; set; }
        public int? PublicationId { get; set; }
        public int? MediaId { get; set; }
        public int? MediaTypeId { get; set; }
        public string? S3UrlMedia { get; set; }
        public string? Description { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}
