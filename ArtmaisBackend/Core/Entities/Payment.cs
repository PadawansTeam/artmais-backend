using System;
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

        [Column("idtipopagamento")]
        public int? PaymentTypeID { get; set; }

        [ForeignKey("PaymentTypeID")]
        public PaymentType? PaymentType { get; set; }

        [Column("datahoracriacao")]
        public DateTime CreateDate { get; set; }

        [Column("datahoraatualizacao")]
        public DateTime LastUpdateDate { get; set; }
    }
}
