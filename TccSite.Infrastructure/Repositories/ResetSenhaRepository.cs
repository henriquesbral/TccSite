using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Infrastructure.Repositories
{
    public class ResetSenhaRepository : IResetSenhaRepository
    {
        private readonly DataContext _context;
        public ResetSenhaRepository(DataContext context)
        {
            _context = context;
        }
        public void SalvarNovaSolicitacao(string email, string novaSenha, bool autorizado)
        {
            var novaSolicitacao = new ResetSenha()
            {
                Email = email,
                NovaSenha = novaSenha,
                Autorizado = autorizado,
                DataCadatroSolicitacao = DateTime.Now
            };

            if (novaSolicitacao is not null)
            {
                _context.Add(novaSolicitacao);
                _context.SaveChanges();
            }
        }
    }
}
