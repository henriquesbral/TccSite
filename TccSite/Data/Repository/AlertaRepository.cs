using Microsoft.EntityFrameworkCore;
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

        public List<Alerta> BuscarAlertas()
        {
            return _context.Alerta.ToList();
        }

        public List<Alerta> BuscarDados(DateTime dataInicio, DateTime dataFim, int tipoAlerta)
        {
            var relatorio = _context.Alerta
                .FromSqlRaw("EXEC USP_GerarRelatorioAlertas @p0, @p1, @p2", dataInicio, dataFim, tipoAlerta).ToList();

            return relatorio;
        }

        public Alerta Get(int codAlerta)
        {
            throw new NotImplementedException();
        }
    }
}
