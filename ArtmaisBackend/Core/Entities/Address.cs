using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("endereco")]
    public class Address
    {
        [Key]
        [Column("idendereco")]
        public int AddressID { get; set; }

        [Column("idusuario")]
        public int? UserID { get; set; }

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

        [Column("latitude")]
        public string? Latitude { get; set; }

        [Column("longitude")]
        public string? Longitude { get; set; }
    }
}
