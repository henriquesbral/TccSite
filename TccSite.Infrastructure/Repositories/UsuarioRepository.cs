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

        public Usuario ObterAutenticar(string email)
        {
            return _context.Usuario.Where(x => x.Email == email).FirstOrDefault();
        }

        public Usuario ObterUsuario(int codUsuario)
        {
            return _context.Usuario.Where(x => x.CodUsuario == codUsuario).FirstOrDefault();
        }

        public Usuario ObterUsuarioPorCPF(string cpf)
        {
            return _context.Usuario.FromSqlRaw(
                "SELECT U.* FROM Usuario U " +
                "INNER JOIN PessoaCadastro PC ON PC.CodPessoaCadastro = U.CodPessoaCadastro " +
                "WHERE CPF = {0}", cpf).FirstOrDefault();
        }

        public List<Usuario> GetUsuarios()
        {
            return _context.Usuario.FromSqlRaw("EXECUTE usp_BuscarUsuarios").ToList();
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

        public Usuario ObterUsuarioPorEmail(string email)
        {
            return _context.Usuario.Where(e => e.Email == email).FirstOrDefault();
        }
    }
}
