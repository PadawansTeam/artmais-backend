using Microsoft.AspNetCore.Mvc;

namespace ArtmaisBackend.Core.Users.Request
{
    public class UserRequest
    {
        [FromQuery(Name = "Id")]
        public int? Id { get; set; }
    }
}
