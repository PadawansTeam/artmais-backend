using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("historicopagamento")]
    public class PaymentHistory
    {
        [Key]
        [Column("idhistoricopagamento")]
        public int PaymentHistoryID { get; set; }

        [Column("idpagamento")]
        public int? PaymentID { get; set; }

        [ForeignKey("PaymentID")]
        public Payments? Payment { get; set; }

        [Column("idestadopagamento")]
        public int? PaymentStatusID { get; set; }

        [ForeignKey("PaymentID")]
        public PaymentsStatus? PaymentStatus { get; set; }

        [Column("datahoracriacao")]
        public DateTime? CreateDate { get; set; }
    }
}
