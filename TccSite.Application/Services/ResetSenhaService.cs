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
    public class ResetSenhaService : IResetSenhaService
    {
        private readonly IResetSenhaRepository _repo;
        public ResetSenhaService(IResetSenhaRepository repo)
        {
            _repo = repo;
        }
        public void SalvarNovaSolicitacao(string email, string novaSenha, bool autorizado)
            => _repo.SalvarNovaSolicitacao(email, novaSenha, autorizado);
    }
}
