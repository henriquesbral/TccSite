using TccSite.Models.Entities;

namespace TccSite.Models.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario ObterAutenticar(string email, string senha);
    }
}
