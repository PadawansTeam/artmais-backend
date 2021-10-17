using System.Collections.Generic;

namespace ArtmaisBackend.Core.Publications.Dto
{
    public class PublicationCommentsDto
    {
        public List<CommentDto?> Comments { get; set; }
        public int? CommentsAmount { get; set; }

        public PublicationCommentsDto(List<CommentDto?> comments, int? commentsAmount)
        {
            Comments = comments;
            CommentsAmount = commentsAmount;
        }
        public PublicationCommentsDto() { }
    }
}
