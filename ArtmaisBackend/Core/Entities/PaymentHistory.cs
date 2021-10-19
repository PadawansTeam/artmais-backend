using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("historicopagamento")]
    public class PaymentHistory
    {
        [Key]
        [Column("idproduto")]
        public int PaymentProductID { get; set; }

        [Column("idpagamento")]
        public int PaymentID { get; set; }

        [ForeignKey("PaymentID")]
        public Payment? Payment { get; set; }

        [Column("estadopagamento")]
        public int Status { get; set; }

        [Column("datahoraatualizacao")]
        public int PayDay { get; set; }
    }
}
