using TccSite.Data.Context;
using TccSite.Domain.Entities;

namespace TccSite.Infrastructure.Repository
{
    public class PessoaCadastroRepository
    {
        private readonly DataContext _context;

        public PessoaCadastroRepository(DataContext context)
        {
            _context = context;
        }

        public PessoaCadastro Obter(int id)
        {
            var pessoa = _context.PessoaCadastro.Where(x => x.CodPessoaCadastro == id).FirstOrDefault();
            return pessoa;
        }
    }
}
