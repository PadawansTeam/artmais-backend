using System;
using System.ComponentModel.DataAnnotations;

namespace artmais_backend.Core.SignUp
{
    public class SignUpRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        public string Description { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Category { get; set; }
        
        [Required]
        public string Subcategory { get; set; }

        public int SubcategoryID { get; set; }
    }
}
