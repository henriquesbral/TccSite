using TCCAPIESP32.Infrastructure.Data;
using TCCAPIESP32.Domain.Entities;

namespace TCCAPIESP32.Application.Services
{
    public class LogImagensEsp32Service
    {
        private readonly AppDbContext _context;

        public LogImagensEsp32Service(AppDbContext context)
        {
            _context = context;
        }

        public void SalvarLog(LogImagensEsp32 log)
        {
            _context.LogImagensEsp32.Add(log);
            _context.SaveChanges();
        }

        public List<LogImagensEsp32> Obter()
        {
            return _context.LogImagensEsp32.ToList();
        }
    }
}
