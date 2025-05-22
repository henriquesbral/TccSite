using TccSite.Models.Entities;

namespace TccSite.Models.Interfaces
{
    public interface IAlertaRepository
    {
        Alerta Get(int codAlerta);
    }
}
