using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("autorizacaoexterna")]
    public class ExternalAuthorization
    {
        [Key]
        [Column("idautorizacaoexterna")]
        public string ExternalAuthorizationId { get; set; }

        [Column("idusuario")]
        public long UserId { get; set; }
    }
}
