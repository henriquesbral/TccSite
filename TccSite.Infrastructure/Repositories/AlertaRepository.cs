using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using TccSite.Data.Context;
using TccSite.Domain.Entities;

namespace TccSite.Infrastructure.Repository
{
    public class AlertaRepository
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

        public List<Alerta> BuscarDados(DateTime dataInicio, DateTime dataFim, int tipoAlerta, int tipoRelatorio)
        {
            using var connection = _context.Alerta.GetDbConnection();

            // Abre a conexão se ainda não estiver aberta
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            var parametros = new
            {
                DataInicio = dataInicio,
                DataFim = dataFim,
                TipoAlerta = tipoAlerta,
                TipoRelatorio = tipoRelatorio
            };

            var relatorio = connection.Query<Alerta>(
                "USP_GerarRelatorioAlertas",
                parametros,
                commandType: CommandType.StoredProcedure
            ).ToList();

            return relatorio;
        }

        public Alerta Get(int codAlerta)
        {
            var alerta = _context.Alerta.Where(x => x.CodAlerta == codAlerta).FirstOrDefault();
            return alerta;
        }
    }
}
