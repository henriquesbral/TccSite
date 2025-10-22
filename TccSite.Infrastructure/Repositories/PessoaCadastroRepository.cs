using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Infrastructure.Repository
{
    public class PessoaCadastroRepository : IPessoaCadastroRepository
    {
        private readonly DataContext _context;

        public PessoaCadastroRepository(DataContext context)
        {
            _context = context;
        }

        public void AtualizarPessoaCadastro(PessoaCadastro pessoa)
        {
            _context.PessoaCadastro.Update(pessoa);
            _context.SaveChanges();
        }

        public PessoaCadastro Obter(int id)
        {
            var pessoa = _context.PessoaCadastro.Where(x => x.CodPessoaCadastro == id).FirstOrDefault();
            return pessoa;
        }
    }
}
