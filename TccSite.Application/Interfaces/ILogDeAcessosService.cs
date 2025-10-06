using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface ILogDeAcessosService
    {
        void Add(LogDeAcessos logDeAcessos);

        void Update(LogDeAcessos newLog);

        LogDeAcessos Obter(int codUsuario);
    }
}
