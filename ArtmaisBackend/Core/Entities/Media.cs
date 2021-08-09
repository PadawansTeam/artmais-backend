using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("midia")]
    public class Media
    {
        [Key]
        [Column("idmidia")]
        public int MediaID { get; set; }

        [Column("idtipomidia")]
        public int? MediaTypeID { get; set; }

        [ForeignKey("MediaTypeID")]
        public MediaType? MediaType { get; set; }

        [Column("idusuario")]
        public long? UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [Column("linkmidia")]
        public string? S3UrlMedia { get; set; }

    }
}
