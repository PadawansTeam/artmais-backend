using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("usuario")]
    public class User
    {
        [Column("idusuario")]
        [Key]
        public int UserID { get; set; }

        [Column("idsubcategoria")]
        public int SubcategoryID { get; set; }

        [ForeignKey("SubcategoryID")]
        public Subcategory Subcategory { get; set; }

        [Column("nome")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("senha")]
        public string Password { get; set; }

        [Column("descricao")]
        public string Description { get; set; }

        [Column("nomeusuario")]
        public string Username { get; set; }

        [Column("datanasc")]
        public DateTime BirthDate { get; set; }

        [Column("datacadastro")]
        public DateTime RegisterDate { get; set; }

        [Column("role")]
        public string Role { get; set; }
    }
}
