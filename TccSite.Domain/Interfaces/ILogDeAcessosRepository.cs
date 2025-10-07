using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Domain.Entities;

namespace TccSite.Domain.Interfaces
{
    public interface ILogDeAcessosRepository
    {
        void Add(LogDeAcessos logDeAcessos);

        void Update(LogDeAcessos newLog);

        LogDeAcessos Obter(int codUsuario);
    }
}
