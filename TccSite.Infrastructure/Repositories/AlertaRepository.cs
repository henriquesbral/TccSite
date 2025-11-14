using Dapper;
using Microsoft.Data.SqlClient;
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
            return _context.Alerta.Where(x => x.DataCadastro >= DateTime.Now.AddHours(-2)).ToList();
        }

        public List<Relatorios> GerarRelatorio(DateTime dataInicio, DateTime dataFim, int? tipoAlerta)
        {
            DateTime dataInicioSql;
            DateTime dataFimSql;
            var alertas = new List<Relatorios>();

            if (tipoAlerta != 0 && tipoAlerta is not null)
            {
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

                var sql = "EXEC dbo.USP_GerarRelatorioAlertas @DataInicio = {0}, @DataFim = {1}, @TipoAlerta = {2}";

                alertas = _context.Set<Relatorios>()
                    .FromSqlRaw(sql, dataInicioSql, dataFimSql, tipoAlerta)
                    .AsNoTracking()
                    .ToList();
            }
            else
            {
                if (dataInicio != null && dataFim != null && dataInicio.Year != DataMinima.Year)
                {
                    dataInicioSql = dataInicio.AddTicks(-(dataInicio.Ticks % TimeSpan.TicksPerSecond));
                    dataFimSql = dataFim.AddTicks(-(dataFim.Ticks % TimeSpan.TicksPerSecond));
                    tipoAlerta = null;
                }
                else
                {
                    dataInicioSql = DateTime.Now.AddDays(-90);
                    dataFimSql = DateTime.Now;
                    tipoAlerta = null;
                }

                var sql = "EXEC dbo.USP_GerarRelatorioAlertas @DataInicio = {0}, @DataFim = {1}, @TipoAlerta = {2}";
                alertas = _context.Set<Relatorios>()
                    .FromSqlRaw(sql, dataInicioSql, dataFimSql, tipoAlerta)
                    .AsNoTracking()
                    .ToList();
            }

            return alertas;
        }

        public List<RelatorioNivelRio> GerarRelatorioNivelRio(DateTime dataInicio, DateTime dataFim)
        {
            // Ajusta datas para remover ticks extras
            DateTime dataInicioSql = dataInicio != DateTime.MinValue
                ? dataInicio.AddTicks(-(dataInicio.Ticks % TimeSpan.TicksPerSecond))
                : DateTime.Now.AddDays(-90);

            DateTime dataFimSql = dataFim != DateTime.MinValue
                ? dataFim.AddTicks(-(dataFim.Ticks % TimeSpan.TicksPerSecond))
                : DateTime.Now;

            var sql = "EXEC dbo.usp_BuscarNivelRio @DataInicio = {0}, @DataFim = {1}";
            // Executa procedure
            var dadosNivelRio = _context.Set<RelatorioNivelRio>()
                    .FromSqlRaw(sql, dataInicioSql, dataFimSql)
                    .AsNoTracking()
                    .ToList();


            return dadosNivelRio;
        }

        public Alerta Get(int codAlerta)
        {
            var alerta = _context.Alerta.Where(x => x.CodAlerta == codAlerta).FirstOrDefault();
            return alerta;
        }
    }
}
