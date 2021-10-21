using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("recomendacao")]
    public class Recomendation
    {
        [Key]
        [Column("idrecomendacao")]
        public long RecomendationID { get; set; }

        [Column("idsubcategoria")]
        public int SubcategoryID { get; set; }

        [Column("SubcategoryID")]
        public Subcategory Subcategory { get; set; }

        [Column("idinteresse")]
        public int InterestID { get; set; }

        [Column("InterestID")]
        public Interest Interest { get; set; }
    }
}
