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
        Usuario ObterAutenticar(string email);

        Usuario ObterUsuario(int codUsuario);

        Usuario ObterUsuarioPorCPF(string cpf);

        Usuario ObterUsuarioPorEmail(string email);

        List<Usuario> GetUsuarios();

        void AdicionarUsuario(Usuario usuario);

        void AtualizarUsuario(Usuario usuario);

        void RemoverUsuario(int id);
    }
}
