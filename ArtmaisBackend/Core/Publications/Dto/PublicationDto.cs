using ArtmaisBackend.Core.Publications.Dto;
using System;
using System.Collections.Generic;

namespace ArtmaisBackend.Core.Portfolio.Dto
{
    public class PublicationDto
    {
        public long? UserId { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? UserPicture { get; set; }
        public string? BackgroundPicture { get; set; }
        public string? Category { get; set; }
        public string? Subcategory { get; set; }
        public string? UserFacebook { get; set; }
        public string? UserInstagram { get; set; }
        public string? UserTwitter { get; set; }
        public string? PublicationFacebook { get; set; }
        public string? PublicationTwitter { get; set; }
        public string? PublicationWhatsapp { get; set; }
        public string? S3UrlMedia { get; set; }
        public string? Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public List<CommentDto?> Comments { get; set; }
        public int? CommentsAmount { get; set; }
        public int? LikesAmount { get; set; }
        public bool IsLiked { get; set; }
    }
}
