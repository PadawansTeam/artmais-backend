using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("acessoperfil")]
    public class ProfileAccess
    {
        [Key]
        [Column("idacessoperfil")]
        public long ProfileAcessId { get; set; }

        [Column("idusuariovisitante")]
        public long VisitorUserId { get; set; }

        [ForeignKey("VisitorUserId")]
        public User VisitorUser { get; set; }

        [Column("idusuariovisitado")]
        public long VisitedUserId { get; set; }

        [ForeignKey("VisitedUserId")]
        public User VisitedUser { get; set; }

        [Column("datavisita")]
        public DateTime VisitDate { get; set; }
    }
}
