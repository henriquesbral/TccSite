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
            var sb = new StringBuilder();
            sb.Append("EXEC USP_GerarRelatorioAlertas");
            sb.Append($"@DataInicio = {dataInicio}");
            sb.Append($"@DataFim = {dataFim}");

            var sql = sb.ToString();

            var alertas = _context.Set<Relatorios>().FromSqlRaw(sql).AsNoTracking().ToList();

            return alertas;
        }

        public Alerta Get(int codAlerta)
        {
            var alerta = _context.Alerta.Where(x => x.CodAlerta == codAlerta).FirstOrDefault();
            return alerta;
        }
    }
}
