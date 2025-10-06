using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface IStatusAlertaService
    {
        StatusAlerta GetAlerta(int codstatus);
    }
}
