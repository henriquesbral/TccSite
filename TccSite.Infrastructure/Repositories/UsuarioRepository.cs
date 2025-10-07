using Microsoft.EntityFrameworkCore;
using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Infrastructure.Repository
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
        public Usuario ObterUsuario(int codUsuario)
        {
            return _context.Usuario.Where(x => x.CodUsuario == codUsuario).FirstOrDefault();
        }

        public List<Usuario> GetUsuarios()
        {
            return _context.Usuario.ToList();
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            _context.SaveChanges();
        }

        public void RemoverUsuario(int id)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
                _context.SaveChanges();
            }
        }

    }
}
