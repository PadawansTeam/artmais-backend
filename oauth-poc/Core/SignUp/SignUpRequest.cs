using System.ComponentModel.DataAnnotations;

namespace oauth_poc.Core.SignUp
{
    public class SignUpRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }
        [Required]
        public string NomeSocial { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
