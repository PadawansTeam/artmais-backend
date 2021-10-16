using ArtmaisBackend.Core.Entities;
using System.Collections.Generic;

namespace ArtmaisBackend.Core.Portfolio.Dto
{
    public class PublicationCommentsDto
    {
        public List<Comment?> Comments { get; set; }
        public int? CommentsAmount { get; set; }

        public PublicationCommentsDto(List<Comment?> comments, int? commentsAmount)
        {
            Comments = comments;
            CommentsAmount = commentsAmount;
        }
        public PublicationCommentsDto() { }
    }
}
