using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace oauth_poc.Core.Entities
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id")]
        [Key]
        public int ID { get; set; }

        [Column("nome")]
        public string Name { get; set; }

        [Column("sobrenome")]
        public string Surname { get; set; }

        [Column("nomesocial")]
        public string SocialName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("senha")]
        public string Password { get; set; }

        [Column("datacadastro")]
        public DateTime RegisterDate { get; set; }
    }
}
