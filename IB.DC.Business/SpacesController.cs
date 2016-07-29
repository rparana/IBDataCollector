using IB.DC.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IB.DC.Model.Entity;
using IB.DC.Data;
using System.Net;
using System.IO;

namespace IB.DC.Business
{
    public class SpacesController : ISpacesController
    {
        private String _url = "";

        public List<Space> ColletarDados(string url)
        {

            List<Space> _spaces = new List<Space>();
            var db = new Contexto();

            var sp = new Space();

            var sc = new SpacesController();

            List<int> vagastemp= new List<int> ();
            /*
            HttpWebRequest requisicao = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resposta = (HttpWebResponse)requisicao.GetResponse();

            int cont;
            byte[] buffer = new byte[1000];
            StringBuilder sb = new StringBuilder();
            string temp;

            Stream stream = resposta.GetResponseStream();

            do
            {
                cont = stream.Read(buffer, 0, buffer.Length);
                temp = Encoding.Default.GetString(buffer, 0, cont).Trim();
                sb.Append(temp);

            } while (cont > 0);

            string pagina = sb.ToString();
            */
            /*
            if (pagina.IndexOf("<font color=\"black\">CEP NAO ENCONTRADO</font>") >= 0)
            {
                //string logradouro = Regex.Match(pagina, "<td width=\"268\" style=\"padding: 2px\">(.*)</td>").Groups[1].Value;
            }
            */
            for (int i = 1; i <= 500; i++)
            {
                vagastemp.Add(i);
            }
            Random rnd = new Random();
            var randomNumbers = Enumerable.Range(1, vagastemp.Count).OrderBy(i => rnd.Next()).ToArray();
            sp.andar = "1 SS";
            sp.vagas_disponiveis = randomNumbers[10];
            sp.vagas_totais = 500;
            sp.vagas_ocupadas = sp.vagas_totais - sp.vagas_disponiveis;
            Salvar(sp);

            _spaces.Add(sp);

            return _spaces;
        }

        public void Excluir(Space obj)
        {
            throw new NotImplementedException();
        }

        public List<Space> ListarPorAndar(long andar)
        {
            throw new NotImplementedException();
        }

        public List<Space> ListarPorPeriodo(DateTime dt_ini, DateTime dt_fim)
        {
            throw new NotImplementedException();
        }

        public List<Space> ListarTudo()
        {
            throw new NotImplementedException();
        }

        public Space Salvar(Space obj)
        {
            var db = new Contexto();

            db.Spaces.Add(obj);
            db.SaveChanges();
            return obj;
        }
    }
}
