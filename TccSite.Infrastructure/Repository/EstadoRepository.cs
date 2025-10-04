using TccSite.Data.Context;
using TccSite.Models.Entities;
using TccSite.Models.Interfaces;

namespace TccSite.Infrastructure.Repository
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly DataContext _context;

        public EstadoRepository(DataContext context)
        {
            _context = context;
        }

        public Estado GetUf()
        {
            throw new NotImplementedException();
        }
    }
}
