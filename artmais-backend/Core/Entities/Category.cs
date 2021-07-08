using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace artmais_backend.Core.Entities
{
    [Table("categoria")]
    public class Category
    {
        [Key]
        [Column("idcategoria")]
        public int CategoryID { get; set; }

        [Column("categoriausuario")]
        public string UserCategory { get; set; }

        [Column("categoriaoutro")]
        public int OtherCategory { get; set; }
    }
}
