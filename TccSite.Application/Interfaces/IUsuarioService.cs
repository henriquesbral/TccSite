using TccSite.Domain.DTOs;
using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface IUsuarioService
    {
        Usuario ObterAutenticar(string email);

        Usuario ObterUsuario(int codUsuario);

        Usuario ObterUsuarioPorCPF(string cpf);

        Usuario ObterUsuarioPorEmail(string email);

        List<Usuario> GetUsuarios();

        List<UsuarioDTO> GetUsuarioDTOs();

        void AdicionarUsuario(Usuario usuario);

        void AtualizarUsuario(Usuario usuario);

        void RemoverUsuario(int id);
    }
}
