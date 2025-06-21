using TccSite.Models.Entities;

namespace TccSite.Models.Interfaces
{
    public interface IAlertaRepository
    {
        Alerta Get(int codAlerta);

        List<Alerta> BuscarDados(DateTime dataInicio, DateTime dataFim, int tipoAlerta);
    }
}
