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
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _repo;
        public CidadeService(ICidadeRepository repo)
        {
            _repo = repo;
        }

        public List<Cidade> GetCidades() 
            => _repo.GetCidades();

        public Cidade GetCidadePorId(int id) 
            => _repo.GetCidadePorId(id);

        public Cidade GetCidadePorNome(string nomeCidade) 
            => _repo.GetCidadePorNome(nomeCidade);
    }
}
