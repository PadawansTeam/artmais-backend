using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace oauth_poc.Core.Entities
{
    [Table("Usuario")]
    public class Usuario
    {
        [Column("ID")]
        [Key]
        public int ID { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Senha")]
        public string Senha { get; set; }
    }
}
