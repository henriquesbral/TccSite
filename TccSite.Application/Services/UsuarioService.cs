using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Application.Interfaces;
using TccSite.Domain.Entities;
using TccSite.Domain.ViewModels;
using TccSite.Infrastructure.Repository;

namespace TccSite.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioRepository _repo;
        public UsuarioService(UsuarioRepository repo)
        {
            _repo = repo;
        }
        public Task AdicionarUsuarioAsync(Usuario usuario)
            => _repo.AdicionarUsuarioAsync(usuario);

        public Task AtualizarUsuarioAsync(Usuario usuario)
            => _repo.AtualizarUsuarioAsync(usuario);

        //public Task<UsuarioViewModel> GetUsuarioByIdAsync(int id)
        //    => _repo.GetUsuarioByIdAsync(id);

        //public Task<List<UsuarioViewModel>> GetUsuariosAsync()
        //    => _repo.GetUsuariosAsync();

        public Usuario ObterAutenticar(string email, string senha)
            => _repo.ObterAutenticar(email, senha);

        public Usuario ObterUsuario(int codUsuario)
            => _repo.ObterUsuario(codUsuario);

        public Task RemoverUsuarioAsync(int id)
            => _repo.RemoverUsuarioAsync(id);
    }
}
