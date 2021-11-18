using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("tipopagamento")]
    public class PaymentType
    {
        [Key]
        [Column("idtipopagamento")]
        public int PaymentTypeID { get; set; }

        [Column("tipopagamento")]
        public string? Description { get; set; }
    }
}
