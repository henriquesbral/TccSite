using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Infrastructure.Repository
{
    public class StatusAlertaRepository : IStatusAlertaRepository
    {
        private readonly DataContext _context;

        public StatusAlertaRepository(DataContext context)
        {
            _context = context;
        }

        public StatusAlerta GetAlerta(int codstatus)
        {
            var status = _context.StatusAlerta.Where(x => x.CodStatusAlerta == codstatus).FirstOrDefault();
            return status;
        }
    }
}
