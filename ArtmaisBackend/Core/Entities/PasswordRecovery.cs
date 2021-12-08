using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("recuperacaosenha")]
    public class PasswordRecovery
    {
        [Column("idrecuperacaosenha")]
        public long PasswordRecoveryID { get; set; }

        [Column("idusuario")]
        public long UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Column("codigo")]
        public int Code { get; set; }

        [Column("dataexpiracao")]
        public DateTime ExpirationDate { get; set; }
    }
}
