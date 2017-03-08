using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB.DC.Model.Entity
{
    [Table("Paginas")]
    public class Pagina
    {
        [Key]
        public long ID { get; set; }
        public string conteudo { get; set; }

    public Pagina()
    {
    }

    }

}
