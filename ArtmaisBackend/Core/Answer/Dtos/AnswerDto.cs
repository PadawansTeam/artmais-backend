using System;

namespace ArtmaisBackend.Core.Answer.Dtos
{
    public class AnswerDto
    {
        public long AnswerID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string UserPicture { get; set; }
        public string Description { get; set; }
        public DateTime? AnswerDate { get; set; }
    }
}
