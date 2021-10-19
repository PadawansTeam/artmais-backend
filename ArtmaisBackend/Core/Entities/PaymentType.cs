using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Entities
{
    [Table("estadopagamento")]
    public class PaymentType
    {
        [Key]
        [Column("idtipopagamento")]
        public int PaymentTypeID { get; set; }

        [Column("tipopagamento")]
        public string? Description { get; set; }
    }
}
