using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("estadopagamento")]
    public class PaymentsStatus
    {
        [Key]
        [Column("idestadopagamento")]
        public int PaymentStatusID { get; set; }

        [Column("estadopagamento")]
        public string? Description { get; set; }
    }
}
