using TccSite.Models.Entities;

namespace TccSite.Models.Interfaces
{
    public interface IConfiguracoesRepository
    {
        Task<Configuracoes> GetConfiguracaoAsync();
        Task AtualizarConfiguracaoAsync(Configuracoes config);
    }

}
