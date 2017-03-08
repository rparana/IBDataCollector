using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IB.DC.Model.Entity
{
    public class ListAndar
    {
        [XmlArrayItem("nome")]
        public string[] andar;
    }
}
