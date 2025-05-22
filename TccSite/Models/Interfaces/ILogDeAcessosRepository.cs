using TccSite.Models.Entities;

namespace TccSite.Models.Interfaces
{
    public interface ILogDeAcessosRepository
    {
        void Add(LogDeAcessos logDeAcessos);

        void Update(LogDeAcessos newLog);

        LogDeAcessos Obter(int codUsuario);
    }
}
