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
    public class PessoaCadastroService : IPessoaCadastroService
    {
        private readonly PessoaCadastroRepository _repo;

        public PessoaCadastroService(PessoaCadastroRepository repo)
        {
            _repo = repo;
        }

        public PessoaCadastro Obter(int id) 
            => _repo.Obter(id);
    }
}
