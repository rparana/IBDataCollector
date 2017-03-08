using IB.DC.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using IB.DC.Model.Entity;
using IB.DC.Data;
using System.Net;
using System.Text.RegularExpressions;

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

            var pg = new Pagina();

            string pagina = "";

            List <int> vagastemp= new List<int> ();
            try
            {
                WebClient _cli = new WebClient(); //Criação do componente para carregamento da Pagina HTML
                pagina = _cli.DownloadString(url); //Leitura da pagina HTML
            }
            catch (Exception e)
            {
                throw e;
            }

            pg.conteudo = pagina;
            SalvarPagina(pg);
            string _vagasTotais = "Vazio";
            string _vagasLivres = "Vazio";
            string _andar = "Vazio";
            if (pagina.IndexOf("<input type=\"text\" name=\"{DT_30_") >= 0)
            {
                //logradouro = Regex.Match(pagina, "<input name=\"{DT_30_.*? value=\"(.*)\".*? > ").Groups[1].Value;
                string pattern1 = "<input type=\"text\" name=\"{DT_30_.*?=\"(.+?)\""; //pattern para vagas totais
                string pattern2 = "<div class=\"id2\">(.+?)</div>"; //pattern para andar
                string pattern3 = "<input type=\"text\" name=\"{DT_31_.*?=\"(.+?)\""; //pattern para vagas livres

                MatchCollection matches1 = Regex.Matches(pagina, pattern1);
                MatchCollection matches2 = Regex.Matches(pagina, pattern2);
                MatchCollection matches3 = Regex.Matches(pagina, pattern3);
                //MessageBox.Show("Matches found: " + matches3.Count);

                for (int i = 0; i < matches1.Count; i++)
                {
                    sp = new Space();
                    sp.andar = matches2[i + 1].Groups[1].Value.Trim();
                    sp.vagas_totais = long.Parse(matches1[i].Groups[1].Value.Trim());
                    sp.vagas_disponiveis = long.Parse(matches3[i].Groups[1].Value.Trim());
                    sp.vagas_ocupadas = sp.vagas_totais - sp.vagas_disponiveis;
                    Salvar(sp);
                    _spaces.Add(sp);
                }
            }

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

        public Pagina SalvarPagina(Pagina obj)
        {
            var db = new Contexto();

            db.Paginas.Add(obj);
            db.SaveChanges();
            return obj;
        }

        List<Space> ISpacesController.ColletarDados(string url)
        {
            throw new NotImplementedException();
        }

        void ISpacesController.Excluir(Space obj)
        {
            throw new NotImplementedException();
        }

        List<SpaceReport> ISpacesController.GerarRelatorio(int tipo, DateTime dt_ini, DateTime dt_fim, List<SpaceReport> obj)
        {
            var db = new Contexto();
            List<SpaceReport> retorno = new List<SpaceReport>();
            string s = "";
            int total_registros = 0;
            long vagas_ocupadas = 0;
            long vagas_total = 0;
            double vagas = 0;
            double percent = 0;
            var query = from d in db.Spaces select d;
            for (int i = 0; i<obj.Count; i++)
            {
                s = obj[i].andar;
                query = query.Where(d => d.timestamp >= dt_ini && d.timestamp <= dt_fim && d.andar==s);
                total_registros = query.Count();
                vagas_ocupadas = 0;
                vagas_total = 0;
                percent = 0;

                if (query.Count() > 0) { 
                    foreach (Space sp in query)
                    {
                        vagas_ocupadas += sp.vagas_ocupadas;
                        vagas_total += sp.vagas_totais;
                    }
                    vagas = vagas_ocupadas / total_registros;
                    vagas_total = vagas_total / total_registros;
                    double d = 100;
                    if (vagas > 0) { 
                    percent = vagas/ (vagas_total / d);
                    }
                    else
                    {
                        percent = 0;
                    }
                    obj[i].vagas_ocupadas = long.Parse(Math.Round(vagas).ToString());
                    obj[i].vagas_totais = vagas_total;
                    obj[i].vagas_ocupadas_percent = int.Parse(Math.Round(percent).ToString());
                    retorno.Add(obj[i]);
                }
            }

            return retorno;
        }

        List<Space> ISpacesController.ListarPorAndar(long andar)
        {
            throw new NotImplementedException();
        }

        List<Space> ISpacesController.ListarPorPeriodo(DateTime dt_ini, DateTime dt_fim)
        {
            throw new NotImplementedException();
        }

        List<Space> ISpacesController.ListarTudo()
        {
            throw new NotImplementedException();
        }

        Space ISpacesController.Salvar(Space obj)
        {
            throw new NotImplementedException();
        }
    }
}
