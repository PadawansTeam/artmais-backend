using Microsoft.AspNetCore.Http;
using System;

namespace ArtmaisBackend.Core.Users.Request
{
    public class UserRequest
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? BackgroundPicture { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? MainPhone { get; set; }
        public string? SecundaryPhone { get; set; }
        public string? ThirdPhone { get; set; }
    }
}
