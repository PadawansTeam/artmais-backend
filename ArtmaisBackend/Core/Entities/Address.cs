using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace ArtmaisBackend.Core.Entities
{
    [Table("endereco")]
    public class Address
    {
        [Key]
        [Column("idendereco")]
        public int AddressID { get; set; }

        [Column("idusuario")]
        public long? UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [Column("rua")]
        public string? Street { get; set; }

        [Column("numero")]
        public int? Number { get; set; }

        [Column("complemento")]
        public string? Complement { get; set; }

        [Column("bairro")]
        public string? Neighborhood { get; set; }

        [Column("cep")]
        public string? ZipCode { get; set; }

        [Column("estado")]
        public string? State { get; set; }

        [Column("cidade")]
        public string? City { get; set; }
    }
}
