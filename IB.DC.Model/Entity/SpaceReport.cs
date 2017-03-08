using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB.DC.Model.Entity
{
    public class SpaceReport
    {
        public long ID { get; set; }
        public DateTime dt_ini { get; set; }
        public DateTime dt_fim { get; set; }
        public string andar { get; set; }
        public long vagas_totais { get; set; }
        public long vagas_disponiveis { get; set; }
        public long vagas_ocupadas { get; set; }
        public int vagas_ocupadas_percent { get; set; }

        public SpaceReport()
        {
        }
    }
}
