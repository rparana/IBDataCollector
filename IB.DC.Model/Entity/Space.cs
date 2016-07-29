using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB.DC.Model.Entity
{
    [Table("Spaces")]
    public class Space
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "timestamp não pode ser branco.")]
        public DateTime timestamp { get; set; }
        [Required(ErrorMessage = "Andar não pode ser branco.")]
        public string andar { get; set; }
        public long vagas_totais { get; set; }
        public long vagas_disponiveis { get; set; }
        public long vagas_ocupadas { get; set; }

        public Space()
        {
            this.timestamp = DateTime.Now;
        }
    }
}
