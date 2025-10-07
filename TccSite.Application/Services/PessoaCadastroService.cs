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
    public class PessoaCadastroService : IPessoaCadastroService
    {
        private readonly IPessoaCadastroRepository _repo;

        public PessoaCadastroService(IPessoaCadastroRepository repo)
        {
            _repo = repo;
        }

        public PessoaCadastro Obter(int id) 
            => _repo.Obter(id);
    }
}
