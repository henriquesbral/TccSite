using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Domain.Entities;

namespace TccSite.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario ObterAutenticar(string email, string senha);

        Usuario ObterUsuario(int codUsuario);

        List<Usuario> GetUsuarios();

        void AdicionarUsuario(Usuario usuario);

        void AtualizarUsuario(Usuario usuario);

        void RemoverUsuario(int id);
    }
}
