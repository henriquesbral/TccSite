using TccSite.Data.Context;
using TccSite.Models.Entities;
using TccSite.Models.Interfaces;

namespace TccSite.Data.Repository
{
    public class AlertaRepository : IAlertaRepository
    {
        private readonly DataContext _context;

        public AlertaRepository(DataContext context)
        {
            _context = context;
        }

        public List<Alerta> BuscarDados(DateTime dataInicio, DateTime dataFim, int tipoAlerta)
        {
            var relatorio = new List<Alerta>();

            return relatorio;
        }

        public Alerta Get(int codAlerta)
        {
            throw new NotImplementedException();
        }
    }
}
