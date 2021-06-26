﻿using System.ComponentModel.DataAnnotations;

namespace oauth_poc.Core.SignUp
{
    public class SignUpRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string SocialName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
