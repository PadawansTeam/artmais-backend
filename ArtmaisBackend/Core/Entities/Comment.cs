using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("comentario")]
    public class Comment
    {
        [Key]
        [Column("idcomentario")]
        public int CommentID { get; set; }

        [Column("idusuario")]
        public long? UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [Column("idpublicacao")]
        public int? PublicationID { get; set; }

        [ForeignKey("PublicationID")]
        public Publication? Publication { get; set; }

        [Column("comentario")]
        public string? Description { get; set; }

        [Column("datahora")]
        public DateTime? CommentDate { get; set; }
    }
}
