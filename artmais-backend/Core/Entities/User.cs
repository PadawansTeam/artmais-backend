using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace artmais_backend.Core.Entities
{
    [Table("usuario")]
    public class User
    {
        [Column("idusuario")]
        [Key]
        public int ID { get; set; }

        [Column("idcategoriasubcategoria")]
        public int CategorySubcategoryID { get; set; }

        [ForeignKey("idcategoriasubcategoria")]
        public CategorySubcategory CategorySubcategory { get; set; }

        [Column("nome")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("senha")]
        public string Password { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("datanasc")]
        public DateTime DataNascimento { get; set; }

        [Column("datacadastro")]
        public DateTime RegisterDate { get; set; }

        [Column("role")]
        public string Role { get; set; }
    }
}
