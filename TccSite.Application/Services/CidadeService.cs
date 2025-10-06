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
    public class CidadeService : ICidadeService
    {
        private readonly CidadeRepository _repo;
        public CidadeService(CidadeRepository repo)
        {
            _repo = repo;
        }

        public List<Cidade> GetCidade() 
            => _repo.GetCidade();

        public Cidade GetCidadePorId(int id) 
            => _repo.GetCidadePorId(id);

        public Cidade GetCidadePorNome(string nomeCidade) 
            => _repo.GetCidadePorNome(nomeCidade);
    }
}
