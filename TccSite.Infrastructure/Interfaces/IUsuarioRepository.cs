using TccSite.Models.Entities;
using TccSite.Models.ViewModels;

namespace TccSite.Models.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario ObterAutenticar(string email, string senha);
        Usuario ObterUsuario(int codUsuario);
        Task<List<UsuarioViewModel>> GetUsuariosAsync();
        Task<UsuarioViewModel> GetUsuarioByIdAsync(int id);
        Task AdicionarUsuarioAsync(Usuario usuario);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task RemoverUsuarioAsync(int id);
    }
}
