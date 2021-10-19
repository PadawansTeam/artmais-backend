using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("pagamento")]
    public class Payment
    {
        [Key]
        [Column("idpagamento")]
        public int PaymentID { get; set; }

        [Column("idusuario")]
        public long? UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [Column("tipopagamento")]
        public string? Type { get; set; }
    }
}
