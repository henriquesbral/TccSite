using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Application.Interfaces;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Application.Services
{
    public class LogDeAcessosService : ILogDeAcessosService
    {
        private readonly ILogDeAcessosRepository _repo;

        public LogDeAcessosService(ILogDeAcessosRepository repo)
        {
            _repo = repo;
        }

        public void Add(LogDeAcessos logDeAcessos)
            => _repo.Add(logDeAcessos);

        public LogDeAcessos Obter(int codUsuario)
            => _repo.Obter(codUsuario);

        public void Update(LogDeAcessos newLog)
            => _repo.Update(newLog);
    }
}
