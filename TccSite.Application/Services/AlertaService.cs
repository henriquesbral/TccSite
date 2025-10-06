using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Application.Interfaces;
using TccSite.Domain.Entities;
using TccSite.Infrastructure.Repository;

namespace TccSite.Application.Services
{
    public class AlertaService : IAlertaService
    {
        private readonly AlertaRepository _repo;
        public AlertaService(AlertaRepository repo)
        {
            _repo = repo;
        }

        public List<Alerta> BuscarAlertas() 
            => _repo.BuscarAlertas();

        public Alerta Get(int codAlerta) 
            => _repo.Get(codAlerta);

        public List<Alerta> BuscarDados(DateTime dataInicio, DateTime dataFim, int tipoAlerta, int tipoRelatorio)
            => _repo.BuscarDados(dataInicio, dataFim, tipoAlerta, tipoRelatorio);
    }
}
