using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("produto")]
    public class Product
    {
        [Key]
        [Column("idproduto")]
        public int ProductID { get; set; }

        [Column("idusuario")]
        public long? UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [Column("nomeproduto")]
        public string? Name { get; set; }

        [Column("valorproduto")]
        public decimal? Value { get; set; }
    }
}
