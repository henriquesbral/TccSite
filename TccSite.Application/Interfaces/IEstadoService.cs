using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface IEstadoService
    {
        Estado GetUf(int id);
        List<Estado> GetUfs();
    }
}
