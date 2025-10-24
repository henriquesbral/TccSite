using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Text;
using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Infrastructure.Repository
{
    public class AlertaRepository : IAlertaRepository
    {
        private readonly DataContext _context;
        private DateTime DataMinima = new DateTime(01,01,0001);

        public AlertaRepository(DataContext context)
        {
            _context = context;
        }

        public List<Alerta> BuscarAlertas()
        {
            return _context.Alerta.ToList();
        }

        public List<Relatorios> GerarRelatorio(DateTime dataInicio, DateTime dataFim)
        {
            DateTime dataInicioSql;
            DateTime dataFimSql;
            var alertas = new List<Relatorios>();

            if (dataInicio != null && dataFim != null && dataInicio.Year != DataMinima.Year)
            {
                dataInicioSql = dataInicio.AddTicks(-(dataInicio.Ticks % TimeSpan.TicksPerSecond));
                dataFimSql = dataFim.AddTicks(-(dataFim.Ticks % TimeSpan.TicksPerSecond));
            }
            else
            {
                dataInicioSql = DateTime.Now.AddDays(-90);
                dataFimSql = DateTime.Now;
            }

            var sql = "EXEC dbo.USP_GerarRelatorioAlertas @DataInicio = {0}, @DataFim = {1}";

            alertas = _context.Set<Relatorios>()
                .FromSqlRaw(sql, dataInicioSql, dataFimSql)
                .AsNoTracking()
                .ToList();

            return alertas;
        }

        public Alerta Get(int codAlerta)
        {
            var alerta = _context.Alerta.Where(x => x.CodAlerta == codAlerta).FirstOrDefault();
            return alerta;
        }
    }
}
