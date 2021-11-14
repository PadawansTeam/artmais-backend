using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("pagamentoproduto")]
    public class PaymentProduct
    {
        [Key]
        [Column("idpagamentoproduto")]
        public int PaymentProductID { get; set; }

        [Column("idproduto")]
        public int? ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product? Product { get; set; }

        [Column("idpagamento")]
        public int? PaymentID { get; set; }

        [ForeignKey("PaymentID")]
        public Payments? Payment { get; set; }
    }
}
