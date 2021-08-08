using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("publicacao")]
    public class Publication
    {
        [Key]
        [Column("idpublicacao")]
        public int PublicationID { get; set; }

        [Column("idmidia")]
        public int? MediaID { get; set; }

        [ForeignKey("MediaID")]
        public Media? Media { get; set; }

        [Column("idusuario")]
        public long? UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [Column("descricao")]
        public string? Description { get; set; }

        [Column("datahora")]
        public DateTime PublicationDate { get; set; }
    }
}
