using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("tipomidia")]
    public class MediaType
    {
        [Column("idtipomidia")]
        public int MediaTypeId { get; set; }

        [Column("tipomidia")]
        public string Description { get; set; }
    }
}
