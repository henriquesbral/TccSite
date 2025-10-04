using TccSite.Data.Context;
using TccSite.Models.Entities;
using TccSite.Models.Interfaces;
using System.Linq;

namespace TccSite.Infrastructure.Repository
{
    public class LogDeAcessosRepository : ILogDeAcessosRepository
    {
        private readonly DataContext _context;

        public LogDeAcessosRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(LogDeAcessos logAcesso)
        {
            _context.LogDeAcessos.Add(logAcesso);
            _context.SaveChanges();
        }

        public void Update(LogDeAcessos log)
        {
            _context.LogDeAcessos.Update(log);
            _context.SaveChanges();
        }

        public LogDeAcessos Obter(int codUsuario)
        {
            return _context.LogDeAcessos.Where(x => x.CodUsuario == codUsuario).FirstOrDefault();
        }
    }
}
