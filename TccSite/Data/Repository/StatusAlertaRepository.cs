using TccSite.Data.Context;
using TccSite.Models.Entities;
using TccSite.Models.Interfaces;

namespace TccSite.Data.Repository
{
    public class StatusAlertaRepository : IStatusAlertaRepository
    {
        private readonly DataContext _context;

        public StatusAlertaRepository(DataContext context)
        {
            _context = context;
        }

        public StatusAlerta Get(int codstatus)
        {
            throw new NotImplementedException();
        }
    }
}
