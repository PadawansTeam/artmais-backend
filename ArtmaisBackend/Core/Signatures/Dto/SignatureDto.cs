using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Signatures.Dto
{
    public class SignatureDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPremium { get; set; }
    }
}
