using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Users.Dto
{
    public class UserCategoryDto
    {
        public int UserId { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
    }
}
