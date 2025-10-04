using TccSite.Data.Context;
using TccSite.Models.Entities;
using TccSite.Models.Interfaces;

namespace TccSite.Infrastructure.Repository
{
    public class PessoaCadastroRepository : IPessoaCadastroRepository
    {
        private readonly DataContext _context;

        public PessoaCadastroRepository(DataContext context)
        {
            _context = context;
        }

        public PessoaCadastro Obter()
        {
            throw new NotImplementedException();
        }
    }
}
