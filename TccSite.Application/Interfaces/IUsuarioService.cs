using TccSite.Domain.Entities;
using TccSite.Domain.ViewModels;

namespace TccSite.Application.Interfaces
{
    public interface IUsuarioService
    {
        Usuario ObterAutenticar(string email, string senha);
        Usuario ObterUsuario(int codUsuario);
        //Task<List<UsuarioViewModel>> GetUsuariosAsync();
        //Task<UsuarioViewModel> GetUsuarioByIdAsync(int id);
        Task AdicionarUsuarioAsync(Usuario usuario);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task RemoverUsuarioAsync(int id);
    }
}
