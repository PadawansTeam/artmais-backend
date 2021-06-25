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
        public string Nome { get; set; }

        [Column("sobrenome")]
        public string Sobrenome { get; set; }

        [Column("nomesocial")]
        public string NomeSocial { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("senha")]
        public string Senha { get; set; }

        [Column("datacadastro")]
        public DateTime DataCadastro { get; set; }
    }
}
