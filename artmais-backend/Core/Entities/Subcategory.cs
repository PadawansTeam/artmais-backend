using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace artmais_backend.Core.Entities
{
    [Table("subcategoria")]
    public class Subcategory
    {
        [Key]
        [Column("idsubcategoria")]
        public int ID { get; set; }

        [Column("subcategoriausuario")]
        public string UserSubcategory { get; set; }

        [Column("subcategoriaoutro")]
        public int OtherSubcategory { get; set; }

        public IList<CategorySubcategory> CategorySubcategory { get; set; }
    }
}
