using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{

    [Table("curtida")]
    public class Like
    {
        [Key]
        [Column("idcurtida")]
        public int LikeID { get; set; }

        [Column("idusuario")]
        public long? UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [Column("idpublicacao")]
        public int? PublicationID { get; set; }

        [ForeignKey("PublicationID")]
        public Publication? Publication { get; set; }

        [Column("datacurtida")]
        public DateTime? LikeDate { get; set; }
    }
}
