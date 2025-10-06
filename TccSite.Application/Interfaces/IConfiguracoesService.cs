using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface IConfiguracoesService
    {
        Configuracoes GetConfiguracaoAsync();

        void AtualizarConfiguracaoAsync(Configuracoes config);
    }

}
