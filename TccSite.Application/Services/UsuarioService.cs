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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public Usuario ObterAutenticar(string email)
            => _repo.ObterAutenticar(email);

        public Usuario ObterUsuario(int codUsuario)
            => _repo.ObterUsuario(codUsuario);
        public Usuario ObterUsuarioPorCPF(string cpf)
            => _repo.ObterUsuarioPorCPF(cpf);

        public List<Usuario> GetUsuarios()
            => _repo.GetUsuarios();

        public void AdicionarUsuario(Usuario usuario)
            => _repo.AdicionarUsuario(usuario);

        public void AtualizarUsuario(Usuario usuario)
            => _repo.AtualizarUsuario(usuario);

        public void RemoverUsuario(int id)
            => _repo.RemoverUsuario(id);
    }
}
