using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("subcategoria")]
    public class Subcategory
    {
        [Key]
        [Column("idsubcategoria")]
        public int SubcategoryID { get; set; }

        [Column("subcategoriausuario")]
        public string UserSubcategory { get; set; }

        [Column("subcategoriaoutro")]
        public int OtherSubcategory { get; set; }

        [Column("idcategoria")]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
    }
}
