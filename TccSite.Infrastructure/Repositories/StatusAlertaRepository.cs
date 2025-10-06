using TccSite.Data.Context;
using TccSite.Domain.Entities;

namespace TccSite.Infrastructure.Repository
{
    public class StatusAlertaRepository
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
