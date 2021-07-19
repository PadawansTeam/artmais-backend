using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Entities
{
    public class Contact
    {
        [Key]
        [Column("idcontatos")]
        public int ContactID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Column("facebook")]
        public string Facebook { get; set; }

        [Column("instagram")]
        public string Instagram { get; set; }

        [Column("twitter")]
        public string Twitter { get; set; }

        [Column("telefone_1")]
        public string MainPhone { get; set; }

        [Column("telefone_2")]
        public string SecundaryPhone { get; set; }

        [Column("telefone_3")]
        public string ThirdPhone { get; set; }

    }
}
