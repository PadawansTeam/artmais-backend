using ArtmaisBackend.Core.Answer.Dtos;
using System;
using System.Collections.Generic;

namespace ArtmaisBackend.Core.Publications.Dto
{
    public class CommentDto
    {
        public int CommentID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string UserPicture { get; set; }
        public string Description { get; set; }
        public DateTime? CommentDate { get; set; }
        public List<AnswerDto?> Answers { get; set; }
    }
}
