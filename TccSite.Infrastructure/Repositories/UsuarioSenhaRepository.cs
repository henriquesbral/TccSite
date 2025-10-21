using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Infrastructure.Repositories
{
    public class UsuarioSenhaRepository : IUsuarioSenhaRepository
    {
        private readonly DataContext _context;

        public UsuarioSenhaRepository(DataContext context)
        {
            _context = context;
        }

        public UsuarioSenha ObterPorUsuario(int codUsuario)
        {
            return _context.UsuarioSenha.FirstOrDefault(x => x.CodUsuario == codUsuario);
        }

        public void Adicionar(UsuarioSenha usuarioSenha)
        {
            _context.UsuarioSenha.Add(usuarioSenha);
            _context.SaveChanges();
        }

        public void Atualizar(UsuarioSenha usuarioSenha)
        {
            _context.UsuarioSenha.Update(usuarioSenha);
            _context.SaveChanges();
        }

    }
}
