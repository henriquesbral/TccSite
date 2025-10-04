using Microsoft.EntityFrameworkCore;
using TccSite.Data.Context;
using TccSite.Models.Entities;
using TccSite.Models.Interfaces;
using TccSite.Models.ViewModels;

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

        public async Task<List<UsuarioViewModel>> GetUsuariosAsync()
        {
            return await _context.Usuario
                .Include(u => u.PessoaCadastro)
                .Select(u => new UsuarioViewModel
                {
                    CodUsuario = u.CodUsuario,
                    Nome = u.PessoaCadastro.Nome,
                    Sobrenome = u.PessoaCadastro.Sobrenome,
                    CPF = u.PessoaCadastro.CPF,
                    Telefone = u.PessoaCadastro.Telefone,
                    Email = u.Email,
                    CodPerfilUsuario = u.CodPerfilUsuario,
                    Ativo = u.Ativo,
                    DataCadastro = u.DataCadastro
                }).ToListAsync();
        }

        public async Task<UsuarioViewModel> GetUsuarioByIdAsync(int id)
        {
            var u = await _context.Usuario
                .Include(x => x.PessoaCadastro)
                .FirstOrDefaultAsync(x => x.CodUsuario == id);

            if (u == null) return null;

            return new UsuarioViewModel
            {
                CodUsuario = u.CodUsuario,
                Nome = u.PessoaCadastro.Nome,
                Sobrenome = u.PessoaCadastro.Sobrenome,
                CPF = u.PessoaCadastro.CPF,
                Telefone = u.PessoaCadastro.Telefone,
                Email = u.Email,
                CodPerfilUsuario = u.CodPerfilUsuario,
                Ativo = u.Ativo,
                DataCadastro = u.DataCadastro
            };
        }

        public async Task AdicionarUsuarioAsync(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverUsuarioAsync(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

    }
}
