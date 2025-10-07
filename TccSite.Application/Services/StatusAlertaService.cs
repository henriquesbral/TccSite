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
    public class StatusAlertaService : IStatusAlertaService
    {
        private readonly IStatusAlertaRepository _repo;

        public StatusAlertaService(IStatusAlertaRepository repo)
        {
            _repo = repo;
        }

        public StatusAlerta GetAlerta(int codstatus)
            => _repo.GetAlerta(codstatus);
    }
}
