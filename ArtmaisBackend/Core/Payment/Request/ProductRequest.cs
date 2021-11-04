using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Payment.Request
{
    public class ProductRequest
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
