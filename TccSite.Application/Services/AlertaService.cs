using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Application.Interfaces;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Application.Services
{
    public class AlertaService : IAlertaService
    {
        private readonly IAlertaRepository _repo;
        public AlertaService(IAlertaRepository repo)
        {
            _repo = repo;
        }

        public List<Alerta> BuscarAlertas() 
            => _repo.BuscarAlertas();

        public Alerta Get(int codAlerta) 
            => _repo.Get(codAlerta);

        public List<Relatorios> GerarRelatorio(DateTime dataInicio, DateTime dataFim, int tipoAlerta)
            => _repo.GerarRelatorio(dataInicio, dataFim, tipoAlerta);

        public List<Relatorios> GerarRelatorioNivelRio(DateTime dataInicio, DateTime dataFim)
            => _repo.GerarRelatorioNivelRio(dataInicio, dataFim);

    }
}
