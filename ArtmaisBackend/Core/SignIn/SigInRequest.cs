using System.ComponentModel.DataAnnotations;

namespace ArtmaisBackend.Core.SignIn
{
    public class SigInRequest
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
