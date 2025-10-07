using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Domain.Entities;

namespace TccSite.Domain.Interfaces
{
    public interface IAlertaRepository
    {
        List<Alerta> BuscarAlertas();

        List<Relatorios> GerarRelatorio(DateTime dataInicio, DateTime dataFim);

        Alerta Get(int id);
    }
}
