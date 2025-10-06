using TccSite.Application;
using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface IAlertaService
    {
        List<Alerta> BuscarAlertas();

        Alerta Get(int codAlerta);

        List<Alerta> BuscarDados(DateTime dataInicio, DateTime dataFim, int tipoAlerta, int tipoRelatorio);
    }
}
