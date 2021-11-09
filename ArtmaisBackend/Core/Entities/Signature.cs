using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("assinatura")]
    public class Signature
    {
        [Key]
        [Column("idassinatura")]
        public int SignatureID { get; set; }

        [Column("idusuario")]
        public long UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Column("datainicio")]
        public DateTime StartDate { get; set; }

        [Column("datafim")]
        public DateTime EndDate { get; set; }
    }
}
