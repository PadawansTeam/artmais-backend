using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("resposta")]
    public class Answer
    {
        [Column("idresposta")]
        public long AnswerID { get; set; }

        [Column("idcomentario")]
        public int CommentID { get; set; }

        [Column("CommentID")]
        public Comment Comment { get; set; }

        [Column("resposta")]
        public string Description { get; set; }

        [Column("datahora")]
        public DateTime DateTime { get; set; }

        [Column("idusuario")]
        public long UserID { get; set; }

        [Column("UserID")]
        public User User { get; set; }
    }
}
