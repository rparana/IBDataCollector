using IB.DC.Model.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace IB.DC.Reports
{
    public class ConfigSectionHandler : IConfigurationSectionHandler

    {

        public const string SECTION_NAME = "ListAndar";

        public object Create(object parent, object configContext, XmlNode section)

        {

            string szConfig = section.SelectSingleNode("//ListAndar").OuterXml;

            ListAndar retConf = null;

            if (szConfig != string.Empty || szConfig != null)

            {

                XmlSerializer xsw = new XmlSerializer(typeof(ListAndar));

                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szConfig));

                ms.Position = 0;

                retConf = (ListAndar)xsw.Deserialize(ms);

            }

            return retConf;

        }

    }
}
