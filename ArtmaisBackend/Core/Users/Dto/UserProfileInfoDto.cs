using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Users.Dto
{
    public class UserProfileInfoDto
    {
        public long UserId { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? UserPicture { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? MainPhone { get; set; }
        public string? SecundaryPhone { get; set; }
        public string? ThirdPhone { get; set; }
    }
}
