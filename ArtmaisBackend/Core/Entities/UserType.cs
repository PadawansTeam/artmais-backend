using System.ComponentModel.DataAnnotations.Schema;

namespace ArtmaisBackend.Core.Entities
{
    [Table("tipousuario")]
    public class UserType
    {
        [Column("idtipousuario")]
        public int UserTypeId { get; set; }

        [Column("descricao")]
        public string Description { get; set; }
    }
}
