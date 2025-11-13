using TccSite.Application;
using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface IAlertaService
    {
        List<Alerta> BuscarAlertas();

        Alerta Get(int codAlerta);

        List<Relatorios> GerarRelatorio(DateTime dataInicio, DateTime dataFim, int? tipoAlerta);

        List<RelatorioNivelRio> GerarRelatorioNivelRio(DateTime dataInicio, DateTime dataFim);
    }
}
