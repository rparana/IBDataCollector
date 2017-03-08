using IB.DC.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB.DC.Model.Business
{
    public interface ISpacesController
    {
        Space Salvar(Space obj);
        void Excluir(Space obj);
        List<Space> ListarTudo();
        List<Space> ListarPorPeriodo(DateTime dt_ini, DateTime dt_fim);
        List<Space> ListarPorAndar(long andar);
        List<Space> ColletarDados(string url);
        List<SpaceReport> GerarRelatorio(int tipo, DateTime dt_ini, DateTime dt_fim, List<SpaceReport> obj);
    }
}
