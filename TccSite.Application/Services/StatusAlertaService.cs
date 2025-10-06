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
    public class StatusAlertaService : IStatusAlertaService
    {
        private readonly StatusAlertaRepository _repo;

        public StatusAlertaService(StatusAlertaRepository repo)
        {
            _repo = repo;
        }

        public StatusAlerta GetAlerta(int codstatus)
            => _repo.GetAlerta(codstatus);
    }
}
