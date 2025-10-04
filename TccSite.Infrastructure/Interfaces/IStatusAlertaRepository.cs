using TccSite.Models.Entities;

namespace TccSite.Models.Interfaces
{
    public interface IStatusAlertaRepository
    {
        StatusAlerta Get(int codstatus);
    }
}
