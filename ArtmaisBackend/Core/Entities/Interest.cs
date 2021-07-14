using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("interesse")]
    public class Interest
    {
        [Key]
        [Column("idinteresse")]
        public int InterestID { get; set; }

        [Column("idusuario")]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Column("idsubcategoria")]
        public int SubcategoryID { get; set; }

        [ForeignKey("SubcategoryID")]
        public Subcategory Subcategory { get; set; }
    }
}
