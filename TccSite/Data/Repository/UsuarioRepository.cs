using TccSite.Data.Context;
using TccSite.Models.Entities;
using TccSite.Models.Interfaces;

namespace TccSite.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;

        public UsuarioRepository(DataContext context)
        {
            _context = context;
        }

        public Usuario ObterAutenticar(string email, string senha)
        {
            return _context.Usuario.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();
        }
    }
}
