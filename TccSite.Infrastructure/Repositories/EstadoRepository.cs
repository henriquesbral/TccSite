using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Infrastructure.Repository
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly DataContext _context;

        public EstadoRepository(DataContext context)
        {
            _context = context;
        }

        public Estado GetUf(int id)
        {
            var estado = _context.Estado.Where(x => x.CodEstado == id).FirstOrDefault();
            return estado;
        }

        public List<Estado> GetUfs()
        {
            var estados = _context.Estado.ToList();
            return estados;
        }
    }
}
