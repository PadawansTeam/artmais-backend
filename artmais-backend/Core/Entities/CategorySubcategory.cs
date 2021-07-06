using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace artmais_backend.Core.Entities
{
    [Table("categoriasubcategoria")]
    public class CategorySubcategory
    {
        [Key]
        [Column("idcategoriasubcategoria")]
        public int ID { get; set; }

        [Column("idcategoria")]
        public int CategoryID { get; set; }

        [ForeignKey("idcategoria")]
        public Category Category { get; set; }

        [Column("idsubcategoria")]
        public int SubcategoryID { get; set; }

        [ForeignKey("idsubcategoria")]
        public Subcategory Subcategory { get; set; }
    }
}
