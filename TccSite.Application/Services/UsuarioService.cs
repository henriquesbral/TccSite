using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Domain.DTOs;
using TccSite.Application.Interfaces;
using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly DataContext _context;
        public UsuarioService(DataContext context, IUsuarioRepository repo)
        {
            _repo = repo;
            _context = context;
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

        public Usuario ObterUsuarioPorEmail(string email)
            => _repo.ObterUsuarioPorEmail(email);

        public List<UsuarioDTO> GetUsuarioDTOs()
        {
            return _context.UsuarioDTOs.FromSqlRaw("EXECUTE usp_BuscarUsuariosDTO").ToList();
        }
    }
}
