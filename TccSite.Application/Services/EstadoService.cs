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
    public class EstadoService : IEstadoService
    {
        private readonly IEstadoRepository _repo;

        public EstadoService(IEstadoRepository repo)
        {
            _repo = repo;
        }

        public Estado GetUf(int id) 
            => _repo.GetUf(id);
        public List<Estado> GetUfs() 
            => _repo.GetUfs();
    }
}
