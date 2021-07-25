using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Users.Request
{
    public class PasswordRequest
    {
        public string OldPassword { get; set; }
        public string OldPasswordConfirmation { get; set; }
        public string Password { get; set; }
    }
}
