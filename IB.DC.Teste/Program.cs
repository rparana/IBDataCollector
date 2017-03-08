using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IB.DC.Data;
using IB.DC.Model;
using IB.DC.Model.Entity;
using IB.DC.Business;

namespace IB.DC.Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Contexto();

            var sp = new Space();

            var sc = new SpacesController();

            List<int> vagastemp = new List<int>();

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

            sc.ColletarDados("file://D:/Temp/Senado2.htm");
            Console.Write(sp.ID + "|" + sp.vagas_ocupadas + "|" + sp.timestamp);
            Console.ReadKey();
        }
    }
}
