using System;
using System.ComponentModel.DataAnnotations;

namespace ArtmaisBackend.Core.OAuth.Google
{
    public class OAuthSignUpRequest
    {
        [Required]
        public string ExternalAuthorizationId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Description { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Subcategory { get; set; }

        public int SubcategoryID { get; set; }

        public string UserPicture { get; set; }

        public string BackgroundPicture { get; set; }
    }
}
